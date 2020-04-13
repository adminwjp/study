using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Company.Api.Data;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;
using Z.EntityFramework.Plus;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class TeamController : BaseController<TeamInfo>
    {
        public TeamController(IRepository<TeamInfo> repository, ILogger<TeamController> logger) : base(repository, logger)
        {
        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] TeamInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "pic")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadTeam + "\\" + name, buffer);
                    obj.Img = new ImageInfo()
                    {
                        Name = RandomUtils.Instance.Id,
                        Href = $"{RandomUtils.Instance.Id}.{suffix}",
                        Src = name,
                        Create = true,
                        Type = Core.Team
                    };
                    base.Repository.Save();
                }
            }
            this.AddMiddleExecet(obj);
            obj.CreateDate = DateTime.Now;
         
            base.Repository.Add(obj);
            base.Repository.Save();
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.AddSuccess));
        }
        [HttpPost("edit")]
        public override async Task<ResponseApi> Edit([FromForm] TeamInfo obj)
        {
            if (Request.ContentType != null)
            {
                if (Request.ContentType.Contains("application/json"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                    {
                        Ref(ref obj, reader.ReadToEnd());
                    }
                }
                else if (Request.ContentType.Contains("text/xml"))
                {
                    using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                    Type t = typeof(TeamInfo);
                    XmlSerializer serializer = new XmlSerializer(t);
                    obj = serializer.Deserialize(reader) as TeamInfo;
                }
            }
            TeamInfo old=null;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "pic")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\" + Core.UploadTeam + "\\" + name, buffer);
                    old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Img).Include(it=>it.TeamSources).FirstOrDefault();
                    if (obj.Img == null || !obj.Img.Id.HasValue)
                    {
                        obj.Img = old.Img;
                    }
                    obj.CreateDate = old.CreateDate;
                    if (obj.Img != null)
                    {
                        System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadTeam + "\\" + obj.Img.Src);
                        obj.Img.Name = RandomUtils.Instance.Id;
                        obj.Img.Href = $"{RandomUtils.Instance.Id}.{suffix}";
                        obj.Img.Src = name;
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.Img);
                    }
                    else
                    {
                        obj.Img = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Type = Core.Team,
                            Create = true
                        };
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.Img);
                    }
                    base.Repository.Save();
                }
            }
            else
            {
                old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Img).Include(it => it.TeamSources).FirstOrDefault();
                if (obj.Img == null || !obj.Img.Id.HasValue)
                {
                    obj.Img = old.Img;
                }
                obj.CreateDate = old.CreateDate;
            }
            if (obj.Sources != null && obj.Sources.Length > 0)
            {
                old ??= base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Img).Include(it => it.TeamSources).FirstOrDefault();
                var context = (base.Repository.DbContext as Company.Domain.CompanyDbContext);
                var temps = context.TeamSourceInfos.Where(it => it.Team.Id == obj.Id).Include(it=>it.Social).ToList();
                foreach (var item in obj.Sources)
                {
                    if (temps == null || temps.Count == 0)
                    {
                        var aa = new TeamSourceInfo()
                        {
                            Social = context.Socials.Find(new object[] { item }),
                            Enable = true,
                            Create = true,
                            Team =context.Teams.Find(new object[] { obj.Id})
                        };
                        context.TeamSourceInfos.Add(aa);
                        base.Repository.Save();
                        context.Entry(aa).State = EntityState.Detached;
                    }
                    else
                    {
                        var temp = temps.Find(it => it.Social.Id.Value == item);
                        if (temp != null)
                        {
                            if (!temp.Enable.HasValue || (temp.Enable.HasValue && !temp.Enable.Value))
                            {
                                (base.Repository.DbContext as Company.Domain.CompanyDbContext).TeamSourceInfos.Where(it => it.Id == temp.Id).Update(it => new TeamSourceInfo { Enable = true });
                                 base.Repository.Save();
                            }
                        }
                        else
                        {
                            var aa = new TeamSourceInfo()
                            {
                                Social = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Socials.Find(new object[] { item }),
                                Enable = true,
                                Create = true,
                                Team = context.Teams.Find(new object[] { obj.Id })
                            };
                            context.TeamSourceInfos.Add(aa);
                            base.Repository.Save();
                            context.Entry(aa).State = EntityState.Detached;
                        }
                    }
                }
                base.Repository.Save();
                if (temps != null || temps.Count > 0)
                {
                    foreach (var temp in temps)
                    {
                        bool exists = false;
                        foreach (var item in obj.Sources)
                        {
                            if (temp.Social.Id.Value == item)
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            (base.Repository.DbContext as Company.Domain.CompanyDbContext).TeamSourceInfos.Where(it=>it.Id==temp.Id).Update(it=>new TeamSourceInfo{ Enable=false});
                            base.Repository.Save();
                            // base.Repository.Save();
                        }
                    }
                }
            }
            this.EditMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);

            obj.ModifyDate = DateTime.Now;
            //此更改可能出现异常 团队来源 添加时 受影响  
            base.Repository.Update(obj);
            base.Repository.Save();
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
        }
        protected override void AddMiddleExecet(TeamInfo obj)
        {
            this.Set(obj);
            if (obj.Sources != null && obj.Sources.Length > 0)
            {
                obj.TeamSources ??= new List<TeamSourceInfo>();
                obj.TeamSources.Clear();
                foreach (var item in obj.Sources)
                {
                    obj.TeamSources.Add(new TeamSourceInfo()
                    {
                        Social = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Socials.Find(new object[] { item }),
                        Enable = true,
                        Create = true,
                        Team = obj
                    });
                }
            }
        }
        private void Set(TeamInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Categories.Find(new object[] { obj.Category.Id });
            }
            if (obj.Service != null && obj.Service.Id.HasValue)
            {
                obj.Service = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Services.Find(new object[] { obj.Service.Id });
            }
            
        }
        protected override void EditMiddleExecet(TeamInfo obj)
        {
            this.Set(obj);
        }
        protected override List<TeamInfo> QueryList(TeamInfo obj, int? page, int? size)
        {
            var context = base.Repository.DbContext as Company.Domain.CompanyDbContext;
            //没有全连接 手动查询2次
            var data = (
                from it in context.Teams
                join ca in context.Categories on it.Category.Id equals ca.Id
                join se in context.Services on it.Service.Id equals se.Id
                join im in context.Images on it.Img.Id equals im.Id 
                join ts in 
                (from ts in context.TeamSourceInfos
                join so in context.Socials on ts.Social.Id equals so.Id /*into aa from bb in aa.DefaultIfEmpty()*/  select new { ts.Id, so.Icon })
                 on it.Id equals ts.Id into tss
                from ts1 in tss.DefaultIfEmpty() select new
                {
                    TeamInfo = it,
                    CategoryInfo = new CategoryInfo() { Id = ca.Id, Name = ca.Name, EnglishName = ca.EnglishName },
                    TeamSourceInfos = ts1,
                    src=im.Name,
                    ServiceInfo = new ServiceInfo() { Id = se.Id, Name = se.Name, EnglishName = se.EnglishName },
                }
                ).ToList();
           //整理数据 
            List<TeamInfo> datas = new List<TeamInfo>();
            foreach (var item in data)
            {
                var it = datas.Find(it => it.Id == item.TeamInfo.Id);
                if (it == null)
                {
                    it = item.TeamInfo;
                    datas.Add(it);
                    it.Src = item.src;
                    it.Category = item.CategoryInfo;
                    it.Service = item.ServiceInfo;
                }
                it.Source =item.TeamSourceInfos.Icon;
            }
            //再次查询 来源信息
            foreach (var item in datas)
            {
                //有来源信息 关联则匹配
                if(item.Source!=null)
                {
                    var sources = context.TeamSourceInfos.Where(it => it.Team.Id == item.Id&&it.Enable.HasValue&&it.Enable.Value).Include(it => it.Social).Select(it => it.Social.Icon);
                    if (sources != null && sources.Any())
                    {
                        item.Source = string.Join(",", sources.Select(it => it));
                    }
                }
            }
            return datas;
            //var result = base.Query(QueryFilter(null, obj)).Include(it=>it.Category).Include(it=>it.TeamSources)/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            //return result;
        }
    }
}