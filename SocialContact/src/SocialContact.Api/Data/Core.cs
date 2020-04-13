using SocialContact.Domain.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NHibernate.Criterion;
using NHibernate.Transform;
using SocialContact.Api.Models;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;

namespace SocialContact.Api.Data
{
    
    public class Core
    {
        private readonly RedisCache _redisCache;
        private readonly ILogger<Core> _logger;
        private readonly IMemoryCache _cache;
        private readonly AppSessionFactory _appSessionFactory;
        public Core(RedisCache redisCache,  AppSessionFactory appSessionFactory, IMemoryCache cache, ILogger<Core> logger)
        {
            this._redisCache = redisCache;
            this._logger = logger;
            this._cache = cache;
            this._appSessionFactory = appSessionFactory;
        }
        public void InitialMain()
        {
            using var session = this._appSessionFactory.Session();
            var categoryEntries = session.CreateCriteria<FileCategoryInfo>().SetProjection(new IProjection[] { Projections.Property("Id").As("Id"),
                Projections.Property("Category").As("Category"),Projections.Property("Accept").As("Accept") })
               .SetResultTransformer(new AliasToBeanResultTransformer(typeof(FileCategoryEntry))).List<FileCategoryEntry>();
            if(categoryEntries.Any())
            {
                string json = categoryEntries.ToJson();
                this.PublishFileCategory(json);
            }
            var userFileEntries = session.Query<UserFileInfo>().Where(it=>it.Parent.Id.HasValue).Select(it => new UserFileEntry()
            {
                FileId = it.Id.Value,
                FileName=it.FileId,
                FileSrc = it.Src,
                AbstractUrl=it.Parent.Src
                //UserFileId = it.Id.Value,
                //UserFileName = it.Name,
                //UserFileSrc = it.Src,
                //FileFlag=it.File.Flag
            }).ToList<UserFileEntry>();
            if (userFileEntries.Any())
            {
                string json = userFileEntries.ToJson();
                this.PublishFile(json);
            }
        }
        public void Initial()
        {
            this.SubscribeFileCategory();
            this.SubscribeFile();
        }
        public static string AesKey = "1234567890abcdefghijklmnopqrstuvwxyz";
        public static string AesIv = "abcdefghijklmnopqrstuvwxyz";
        public static string XmlPublicString = "";
        public static string XmlPrivateString = "";
        public static string Index = "SocialContact";
        public static string Type = "file";
        public const string FileCategoryChannel= "file_category";
        public const string FileChannel = "file";
        public void  SubscribeFileCategory() 
        {
            this._redisCache.Subscribe( Core.FileCategoryChannel, (it,msg)=> {
                this._logger.LogInformation($"订阅渠道:{it},订阅文件分类信息:{msg}");
                List<FileCategoryEntry> categoryEntries = JsonUtils.Instance.ToObject<List<FileCategoryEntry>>(msg);
                this._cache.Set(Core.FileCategoryChannel, categoryEntries,new MemoryCacheEntryOptions() {  Priority= CacheItemPriority.NeverRemove});
            });
        }
        public List<FileCategoryEntry> FileCategoryEntries => this._cache.Get<List<FileCategoryEntry>>(Core.FileCategoryChannel) ?? new List<FileCategoryEntry>();
        public List<UserFileEntry> UserFileEntries=> this._cache.Get<List<UserFileEntry>>(Core.FileChannel) ?? new List<UserFileEntry>();
        public void PublishFileCategory(string msg)
        {
            this._redisCache.Publish(Core.FileCategoryChannel, msg);
        }
        public void SubscribeFile()
        {
            this._redisCache.Subscribe(Core.FileChannel, (it, msg) => {
                this._logger.LogInformation($"订阅渠道:{it},订阅文件信息:{msg}");
                List<UserFileEntry> userFileEntries = JsonUtils.Instance.ToObject<List<UserFileEntry>>(msg);
                this._cache.Set(Core.FileChannel, userFileEntries, new MemoryCacheEntryOptions() { Priority = CacheItemPriority.NeverRemove });
            });
        }
        public void PublishFile(string msg)
        {
            this._redisCache.Publish(Core.FileChannel, msg);
        }
    }
}
