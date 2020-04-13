using SocialContact.Domain.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SocialContact.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace SocialContact.Api.Data
{
    /// <summary>
    /// 逻辑不严谨 测试或正式用 的 
    /// </summary>
    public interface IUserFileService
    {
         IUnitWork UnitWork{ get; }
        IMemoryCache Cache { get; }
        ILogger<IUserFileService> Logger { get; }
        Core Core { get; }
        SocialContact.Domain.Core.UserFileInfo ToFile(AdminInfo admin,UserInfo user, FileCategoryInfo fileCategory)
        {
            SocialContact.Domain.Core.UserFileInfo fileInfo = new SocialContact.Domain.Core.UserFileInfo()
            {
                CreateDate = DateTime.Now,
                Category = fileCategory,
                Admin = admin,
                User=user
            };
            return fileInfo;
        }
        void Publish(UserFileEntry userFileEntry)
        {
            List<UserFileEntry> result = this.Cache.Get<List<UserFileEntry>>("file");
            result ??= new List<UserFileEntry>();
            if (result.Any())
            {
                var temp = result.Find(it => it.FileId == userFileEntry.FileId);
                if (temp==null)
                {
                    result.Add(userFileEntry);
                }
                else
                {
                    temp.FileName = userFileEntry.FileName;
                    temp.FileSrc = userFileEntry.FileSrc;
                    temp.AbstractUrl = userFileEntry.AbstractUrl;
                }
            }
            else
            {
                result.Add(userFileEntry);
            }
            this.Cache.Set("file", result);
            this.Core.PublishFile(result.ToJson());
        }
        /// <summary>
        /// 存在父类 添加 则添加子类 否则 父类 子类 都添加  更新则 更新子类 父类不存在则添加 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="channge"></param>
        /// <param name="add"></param>
        /// <param name="update"> 更新时 是否 强制更新 file_id src 用户头像等情况下更新不变</param>
        /// <returns></returns>
        SocialContact.Domain.Core.UserFileInfo AddOrUpdate( SocialContact.Domain.Core.UserFileInfo file,out bool channge, bool add = true,bool update=true)
        {
            channge = false;
            var oldfile = this.UnitWork.FindSingle<SocialContact.Domain.Core.UserFileInfo>(it =>string.IsNullOrEmpty(file.Base64) ?it.Id==file.Id : it.Base64 == file.Base64 );
            Action<SocialContact.Domain.Core.UserFileInfo> publish = (childFile) => {
                var userFileEntry = this.ToUserFileEntry(childFile);
                this.Publish(userFileEntry); 
            };
            Action<SocialContact.Domain.Core.UserFileInfo> updateCategory = (childFile) => {
                if (childFile.Category == null && childFile.Category != null)
                {
                    childFile.Category = file.Category;
                    this.UnitWork.Update(childFile);
                }
            };
            if (oldfile == null)
            {
                Logger.LogInformation("文件不存在");
                file.Id = this.DML(file);//父
                Logger.LogInformation($"添加父文件成功,id:{file.Id}");
                if (add)
                {
                    SocialContact.Domain.Core.UserFileInfo childFile = (SocialContact.Domain.Core.UserFileInfo)file.Clone();

                    childFile.Id = this.DML(childFile);
                    publish(childFile);
                    updateCategory(childFile);
                    Logger.LogInformation($"添加子文件成功,id:{childFile.Id},发布文件信息成功");
                    return childFile;
                }
                else
                {
                    var childFile = this.UnitWork.FindSingle<SocialContact.Domain.Core.UserFileInfo>(it => it.FileId == it.FileId);
                    childFile.Parent = file;
                    this.UnitWork.Update(childFile);
                    publish(childFile); 
                    updateCategory(childFile);
                    Logger.LogInformation($"修改子文件成功,id:{childFile.Id},发布文件信息成功");
                    return childFile;
                }
            }
            else
            {
                if (add)
                {
                    file.Base64 = null;
                    file.Parent = oldfile.Parent??oldfile;
                    file.Id = this.DML(file);
                    publish(file); 
                    updateCategory(file);
                    channge = true;
                    Logger.LogInformation($"父文件存在,id:{file.Id}，添加子文件成功,id:{file.Id},发布文件信息成功");
                    return file;
                }
                else
                {
                    file.Base64 = null;
                    //修改文件时则更新 其他情况下 操作 不更新
                    file.FileId =update? RandomUtils.Instance.OrderId.Sha1():(oldfile.Parent ?? oldfile).FileId;
                    file.Parent = oldfile.Parent ?? oldfile;
                    publish(file);
                    channge = update;
                    this.UnitWork.Update<SocialContact.Domain.Core.UserFileInfo>(it => it.Id == file.Id, it =>new UserFileInfo() { Category=file.Category,Parent = oldfile.Parent ?? oldfile, FileId=file.FileId,Src=file.Src});
                    Logger.LogInformation($"父文件存在,id:{file.Id}，修改子文件成功,id:{file.Id},发布文件信息成功");
                    return file;
                }
            }
        }
        UserFileEntry ToUserFileEntry(SocialContact.Domain.Core.UserFileInfo file)
        {
            return new UserFileEntry() { FileId = file.Id.Value, FileSrc = file.Src,FileName=file.FileId, AbstractUrl=file.Parent.Src };
        }
        int DML<T>(T obj) where T : class
        {
            object result = this.UnitWork.Add(obj);
            int num = (int)result;
            return num;
        }
    }
}
