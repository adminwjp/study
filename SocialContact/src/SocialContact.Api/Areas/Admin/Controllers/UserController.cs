using System.Linq;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using SocialContact.Api.Data;
using SocialContact.Api.Models;
using NHibernate.Criterion;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(Utility.ResponseApi), StatusCodes.Status200OK)]

    public class UserController : SocialContact.Api.Controllers.BaseController<UserInfo, QueryUserFormViewModel, QueryUserInfoResultViewModel>
    {
        private readonly IUserFileService _userFileService;
        public UserController(RedisCache redisCache, IUnitWork unitWork, IMemoryCache cache, IUserFileService userFileService, AuthrizeValidator authrize, ILogger<UserController> logger) : base(redisCache, unitWork, cache,authrize, logger)
        {
            this._userFileService = userFileService;
            base.IsCustomValidator = true;
            base.PageName = "user";
        }
        protected override async Task<ResponseApi> AddMiddlewareExecuted(UserInfo obj)
        {
            obj = this.AddMiddlewareExecute(obj);
            AddOrUpdataUserFile(obj,true);
            obj.Id=(int)base.UnitWork.Add(obj);
			obj.HeadPic.User=obj;
            base.UnitWork.Update(obj.HeadPic);
			obj.CardPhoto1.User=obj;
            base.UnitWork.Update(obj.CardPhoto1);
			obj.CardPhoto2.User=obj;
            base.UnitWork.Update(obj.CardPhoto2);
			obj.HandCardPhoto1.User=obj;
            base.UnitWork.Update(obj.HandCardPhoto1);
			obj.HandCardPhoto2.User=obj;
            base.UnitWork.Update(obj.HandCardPhoto2);
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.AddSuccess);
            return await Task.FromResult(response);
        }
        protected override async Task<ResponseApi> EditMiddlewareExecuted(UserInfo obj)
        {
            this.EditMiddlewareExecute(obj);
            var oldObj = this.UnitWork.FindSingle<UserInfo>(it => it.Id == obj.Id);
            obj = this.UpdateOldObject(obj, ref oldObj);
            AddOrUpdataUserFile(obj, false);
            base.UnitWork.Update(obj);
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess);
            return await Task.FromResult(response);
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryUserFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (!string.IsNullOrEmpty(obj.Token))
            {
                criterias.Add(Expression.Like("Token",$"%{obj.Token}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Account))
            {
                criterias.Add(Expression.Like("Account", $"%{obj.Account}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.NickName))
            {
                criterias.Add(Expression.Like("NickName", $"%{obj.NickName}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.RealName))
            {
                criterias.Add(Expression.Like("RealName", $"%{obj.RealName}%"));
                res = true;
            }
            if (obj.BirthdayDate!=null&&obj.BirthdayDate.Length==2)
            {
                criterias.Add(Expression.Between("Birthday", obj.BirthdayDate[0], obj.BirthdayDate[1]));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Phone))
            {
                criterias.Add(Expression.Like("Phone", $"%{obj.Phone}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Sex))
            {
                criterias.Add(Expression.Eq("Sex", obj.Sex));
                res = true;
            }
            if (obj.EdutionId.HasValue)
            {
                criterias.Add(Expression.Eq("Edution.Id", obj.EdutionId.Value));
                res = true;
            }
            if (obj.MaritalId.HasValue)
            {
                criterias.Add(Expression.Eq("Marital.Id", obj.MaritalId.Value));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Email))
            {
                criterias.Add(Expression.Like("Email", $"%{obj.Email}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.CardId))
            {
                criterias.Add(Expression.Like("CardId", $"%{obj.CardId}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.BankId))
            {
                criterias.Add(Expression.Like("BankId", $"%{obj.BankId}%"));
                res = true;
            }
            if (obj.StatusId.HasValue)
            {
                criterias.Add(Expression.Eq("Status.Id", obj.StatusId.Value));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.LoginIp))
            {
                criterias.Add(Expression.Like("LoginIp", $"%{obj.LoginIp}%"));
                res = true;
            }
            if (obj.LevelId.HasValue)
            {
                criterias.Add(Expression.Eq("Level.Id", obj.LevelId.Value));
                res = true;
            }
            if (obj.LoginDate != null && obj.LoginDate.Length == 2)
            {
                criterias.Add(Expression.Between("LoginDate", obj.LoginDate[0], obj.LoginDate[1]));
                res = true;
            }
            return res;
        }
        void AddOrUpdataUserFile(UserInfo obj,bool add=true)
        {
            obj.HeadPic = Set(obj.HeadPic, obj, add);
            obj.CardPhoto1 = Set(obj.CardPhoto1, obj, add);
            obj.CardPhoto2 = Set(obj.CardPhoto2, obj, add);
            obj.HandCardPhoto1 = Set(obj.HandCardPhoto1, obj, add);
            obj.HandCardPhoto2 = Set(obj.HandCardPhoto2, obj, add);
        }
        UserFileInfo Set(UserFileInfo fileInfo,UserInfo user=null,bool add=true)
        {
            fileInfo.User =add?null: user;
            string str = base.RedisCache.HashGet("file", fileInfo.FileId) ;
            if (add)
            {
                str = str ?? throw new Exception("用户文件不存在");
            }
            UserFileInfo parent =null;
            UserFileInfo pic = null;
            if (str != null)
            {
                string[] strs = str.Split('_');
                int index = str.IndexOf("_");
                fileInfo.Src = str.Substring(0, index);
                fileInfo.Type = str.Substring(index + 1);
                fileInfo.Base64 = Base64Utils.Base64String(System.IO.File.ReadAllBytes($"{AddressConfig.UploadImgDirectory}\\{fileInfo.Src}"));
                parent = base.UnitWork.FindSingle<UserFileInfo>(it => it.Base64 == fileInfo.Base64);
                if (!add)
                {
                    //pic = base.UnitWork.FindSingle<UserFileInfo>(it => it.User.Id == user.Id && it.FileId == fileInfo.FileId && it.Type == fileInfo.Type);
                    pic = base.UnitWork.FindSingle<UserFileInfo>(it =>  it.FileId == fileInfo.FileId && it.Type == fileInfo.Type);
					if (pic == null) throw new Exception("用户文件不存在");
                }
            }
            else
            {
                // pic = base.UnitWork.FindSingle<UserFileInfo>(it => it.User.Id == user.Id && it.FileId == fileInfo.FileId);
				pic = base.UnitWork.FindSingle<UserFileInfo>(it => it.FileId == fileInfo.FileId);
                if (pic == null) throw new Exception("用户文件不存在");
                parent = pic.Parent;
            }
            if (parent == null)
            {
                parent = fileInfo;
                parent.Id = (int)base.UnitWork.Add(parent);
                if (add)
                {
                    pic = (UserFileInfo)parent.Clone();
                    pic.Id = (int)base.UnitWork.Add(pic);
                }
            }
            else
            {
                if (add)
                {
                    pic = fileInfo;
                    pic.Parent = parent;
                    pic.Base64 = null;
                    pic.Id = (int)base.UnitWork.Add(pic);
                }
            }
            if (add)
            {
                var userFileEntry1 = this._userFileService.ToUserFileEntry(pic);
                this._userFileService.Publish(userFileEntry1);
                return pic;
            }
            else
            {
                if (parent != null)
                {
                    if (pic.Parent.Base64 != parent.Base64)
                    {
                        pic.Parent = parent;
                    }
                }
            }
            pic.FileId = RandomUtils.Instance.OrderId.Sha1();
            pic.Src = $"{RandomUtils.Instance.OrderId.Sha1()}.{pic.Parent.Src.Split('.').LastOrDefault()}";
            base.UnitWork.Update<UserFileInfo>(it => it.Id == pic.Id, it => new UserFileInfo()
            {
                Parent = pic.Parent,
                FileId = pic.FileId,
                Src = pic.Src,
                UpdateDate = DateTime.Now
            });
            var userFileEntry = this._userFileService.ToUserFileEntry(pic);
            this._userFileService.Publish(userFileEntry);
            return pic;
        }
        protected override Task<Dictionary<string, List<string>>> CustomValidate(Dictionary<string, List<string>> errors, UserInfo obj, int way = 0)
        {
            if (obj.HeadPic == null || string.IsNullOrEmpty(obj.HeadPic.FileId))
            {
                errors.Add("head_pic.id",new List<string>(){"用户头像不能为空"});
            }
            if (obj.CardPhoto1 == null || string.IsNullOrEmpty(obj.CardPhoto1.FileId))
            {
                errors.Add("card_pic1.id", new List<string>() { "身份证正面不能为空" });
            }
            if (obj.CardPhoto2 == null || string.IsNullOrEmpty(obj.CardPhoto2.FileId))
            {
                errors.Add("card_pic2.id", new List<string>() { "身份证反面不能为空" });
            }
            if (obj.HandCardPhoto1 == null || string.IsNullOrEmpty(obj.HandCardPhoto1.FileId))
            {
                errors.Add("hand_card_pic1.id", new List<string>() { "手持身份证正面不能为空" });
            }
            if (obj.HandCardPhoto2 == null || string.IsNullOrEmpty(obj.HandCardPhoto2.FileId))
            {
                errors.Add("hand_card_pic2.id", new List<string>() { "手持身份证反面不能为空" });
            }
            if (obj.Edution == null || !obj.Edution.Id.HasValue)
            {
                errors.Add("edution.id", new List<string>() { "学历不能为空" });
            }
            if (obj.Marital == null || !obj.Marital.Id.HasValue)
            {
                errors.Add("marital.id", new List<string>() { "婚姻状态不能为空" });
            }
            if (obj.Level == null || !obj.Level.Id.HasValue)
            {
                errors.Add("marital.id", new List<string>() { "用户等级不能为空" });
            }
            return base.CustomValidate(errors, obj, way);
        }
        protected override Func<UserInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Account };
        }
    }
}