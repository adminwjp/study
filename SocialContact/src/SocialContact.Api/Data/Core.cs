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
using Utility.Redis;
using Utility.Json.Extensions;
using Utility.Json;
using System.Text;

namespace SocialContact.Api.Data
{
    
    public class Core
    {
        private readonly IRedisCache _redisCache;
        private readonly ILogger<Core> _logger;
        private readonly IMemoryCache _cache;
        private readonly AppSessionFactory _appSessionFactory;
        public Core(IRedisCache redisCache,  AppSessionFactory appSessionFactory, IMemoryCache cache, ILogger<Core> logger)
        {
            this._redisCache = redisCache;
            this._logger = logger;
            this._cache = cache;
            this._appSessionFactory = appSessionFactory;
        }
        public void InitialMain()
        {
            using var session = this._appSessionFactory.OpenSession();
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
                string str = Encoding.UTF8.GetString(msg);
                this._logger.LogInformation($"订阅渠道:{it},订阅文件分类信息:{str}");
                List<FileCategoryEntry> categoryEntries = JsonHelper.ToObject<List<FileCategoryEntry>>(str);
                this._cache.Set(Core.FileCategoryChannel, categoryEntries,new MemoryCacheEntryOptions() {  Priority= CacheItemPriority.NeverRemove});
            });
        }
        public List<FileCategoryEntry> FileCategoryEntries => this._cache.Get<List<FileCategoryEntry>>(Core.FileCategoryChannel) ?? new List<FileCategoryEntry>();
        public List<UserFileEntry> UserFileEntries=> this._cache.Get<List<UserFileEntry>>(Core.FileChannel) ?? new List<UserFileEntry>();
        public void PublishFileCategory(string msg)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            this._redisCache.Publish(Core.FileCategoryChannel, buffer);
        }
        public void SubscribeFile()
        {
            this._redisCache.Subscribe(Core.FileChannel, (it, msg) => {
                string str = Encoding.UTF8.GetString(msg);
                this._logger.LogInformation($"订阅渠道:{it},订阅文件信息:{str}");
                List<UserFileEntry> userFileEntries = JsonHelper.ToObject<List<UserFileEntry>>(str);
                this._cache.Set(Core.FileChannel, userFileEntries, new MemoryCacheEntryOptions() { Priority = CacheItemPriority.NeverRemove });
            });
        }
        public void PublishFile(string msg)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg);
            this._redisCache.Publish(Core.FileChannel, buffer);
        }
    }
}
