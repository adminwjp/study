using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Domain.Core;
using Utility;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Diagnostics.CodeAnalysis;
using Company.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Company.Api.Data;
using System;
using System.Xml.Serialization;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class CategoryController : BaseController<CategoryInfo>
    {
        public CategoryController(IRepository<CategoryInfo> repository, ILogger<CategoryController> logger) : base(repository, logger)
        {

        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] CategoryInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "background_image")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadBackgroundImage + "\\" + name, buffer);
                    obj.BackgroundImage = new ImageInfo()
                    {
                        Name = RandomUtils.Instance.Id,
                        Href = $"{RandomUtils.Instance.Id}.{suffix}",
                        Src = name,
                        Create = true,
                        Type = Core.Bg
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
        public override async Task<ResponseApi> Edit([FromForm] CategoryInfo obj)
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
                    Type t = typeof(ServiceInfo);
                    XmlSerializer serializer = new XmlSerializer(t);
                    obj = serializer.Deserialize(reader) as CategoryInfo;
                }
            }
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "background_image")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\" + Core.UploadBackgroundImage + "\\" + name, buffer);
                    var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.BackgroundImage).FirstOrDefault();
                    if (obj.BackgroundImage == null || !obj.BackgroundImage.Id.HasValue)
                    {
                        obj.BackgroundImage = old.BackgroundImage;
                    }
                    obj.CreateDate = old.CreateDate;
                    if (obj.BackgroundImage != null)
                    {
                        System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadBackgroundImage + "\\" + obj.BackgroundImage.Src);
                        obj.BackgroundImage.Name = RandomUtils.Instance.Id;
                        obj.BackgroundImage.Href = $"{RandomUtils.Instance.Id}.{suffix}";
                        obj.BackgroundImage.Src = name;
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.BackgroundImage);
                    }
                    else
                    {
                        obj.BackgroundImage = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Type = Core.Bg,
                            Create = true
                        };
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.BackgroundImage);
                    }
                    base.Repository.Save();
                }
            }
            else
            {
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.BackgroundImage).FirstOrDefault();
                if (obj.BackgroundImage == null || !obj.BackgroundImage.Id.HasValue)
                {
                    obj.BackgroundImage = old.BackgroundImage;
                }
                obj.CreateDate = old.CreateDate;
            }
            this.EditMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);
            
            obj.ModifyDate = DateTime.Now;
            base.Repository.Update(obj);
            base.Repository.Save();
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
        }
        protected override void AddMiddleExecet(CategoryInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = (base.Repository.DbContext as Company.Domain.CompanyDbContext).BasicCategories.Find(new object[] { obj.Category.Id });
            }
            base.AddMiddleExecet(obj);
        }
        protected override void EditMiddleExecet(CategoryInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<CategoryInfo> QueryList(CategoryInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it => it.Category).Include(it=>it.BackgroundImage).Select(it=>new CategoryInfo() { 
                Id=it.Id,
                Name=it.Name,
                EnglishName=it.EnglishName,
                Description=it.Description,
                EnglishDescription=it.EnglishDescription,
                CreateDate=it.CreateDate,
                ModifyDate=it.ModifyDate,
                Enable=it.Enable,
                Category=new BasicCategoryInfo()
                {
                    Id = it.Category.Id,
                    Name = it.Category.Name,
                    EnglishName = it.Category.EnglishName
                },
                Bg=it.BackgroundImage.Name
            })/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
        [HttpGet("service_category")]
        public virtual async Task<Utility.ResponseApi> ServiceCategory()
        {
            return await this.GetCategory("service");
        }
        [HttpGet("skill_category")]
        public virtual async Task<Utility.ResponseApi> SkillCategory()
        {
            return await this.GetCategory("skill");
        }
        [HttpGet("testimonial_category")]
        public virtual async Task<Utility.ResponseApi> TestimonialCategory()
        {
            return await this.GetCategory("testimonials");
        }
        [HttpGet("theme_category")]
        public virtual async Task<Utility.ResponseApi> ThemeCategory()
        {
            return await this.GetCategory("theme");
        }
        private   async Task<Utility.ResponseApi> GetCategory(string name)
        {
            Utility.ResponseApi responseApi = ResponseApiUtils.Success(GetLanguage());
            string sql = $"select c.id Id,c.name Name,c.english_name  EnglishName from category_info c inner join basic_category_info b  on b.id=c.category_id and lower(b.english_name)='{name}' group by id";
            var data = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Database.GetDbConnection().Query(sql);
            responseApi.Data = data;
            return await Task.FromResult<Utility.ResponseApi>(responseApi);
        }
        [HttpGet("brand_category")]
        public virtual async Task<Utility.ResponseApi> BrandCategory()
        {
            return await this.GetCategory("brand");
        }
        [HttpGet("work_category")]
        public virtual async Task<Utility.ResponseApi> WorkCategory()
        {
            return await this.GetCategory("work");
        }
        [HttpGet("team_category")]
        public virtual async Task<Utility.ResponseApi> TeamCategory()
        {
            return await this.GetCategory("team");
        }
        [HttpGet("menu_category")]
        public virtual async Task<Utility.ResponseApi> MenuCategory()
        {
            Utility.ResponseApi responseApi = ResponseApiUtils.Success(GetLanguage());
            //var data = base.Repository.Find(null).Include(it => it.Menus).Select(it => new CategoryInfo()
            //{
            //    Id = it.Id,
            //    Name = it.Name,
            //    EnglishName = it.EnglishName,
            //    Menus = (from it1 in it.Menus select new MenuInfo() { Id = it1.Id, Name = it1.Name, EnglishName = it1.EnglishName }).ToList()
            //}).ToList();
            //List<CategoryInfo> temp = new List<CategoryInfo>(data.Count);
            //for (int i = 0; i < data.Count; i++)
            //{
            //    if (data[i].Menus == null||!data[i].Menus.Any())
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        temp.Add(data[i]);
            //    }
            //}
            //responseApi.Data = temp;
            var context = (base.Repository.DbContext as Company.Domain.CompanyDbContext);
            var data = context.Menus
                .Join(context.Categories, it => it.Category.Id, it => it.Id, (it, it1) => new
                {
                    CategoryInfo = new CategoryInfo()
                    {
                        Id = it1.Id,
                        Name = it1.Name,
                        EnglishName = it1.EnglishName
                    },
                    MenuInfo = new MenuInfo() { Id = it.Id, Name = it.Name, EnglishName = it.EnglishName }
                }).ToList();
            responseApi.Data = data;
            List<CategoryInfo> temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    it.Menus = new List<MenuInfo>();
                    temp.Add(it);
                }
                it.Menus.Add(item.MenuInfo);
            }
            responseApi.Data = temp;
            return await Task.FromResult<Utility.ResponseApi>(responseApi);
        }
        [HttpGet("menus")]
        public virtual async Task<Utility.ResponseApi> Menu()
        {
            Utility.ResponseApi responseApi = ResponseApiUtils.Success(GetLanguage());
            var data = GetDynamics();
            responseApi.Data = ParseData(data);
            return await Task.FromResult<Utility.ResponseApi>(responseApi);
        }
        private IQueryable<dynamic> GetDynamics()
        {
            var context = HttpContext.RequestServices.GetService<CompanyDbContext>();
            var data = from menu in context.Menus
                       join child in context.Menus on menu.Id equals
                        child.Id
                       join category in context.Categories on child.Category.Id equals category.Id
                       into items
                       from it in items.DefaultIfEmpty()
                       select new
                       {
                           it,
                           child
                       };
            return data;
        }
        private List<CategoryInfo> ParseData(IQueryable<dynamic> dynamics)
        {
            List<CategoryInfo> categoryInfos = new List<CategoryInfo>();
            foreach (var item in dynamics)
            {
                if (item.it != null)
                {
                    CategoryInfo temp = null;
                    if ((temp = categoryInfos.Find(it => it.Id == item.it.Id)) != null)
                    {
                        //using IEnumerator<MenuInfo> enumerator = item.it.Menus.GetEnumerator();
                        //while (enumerator.MoveNext())
                        //    temp.Menus.Add(enumerator.Current);
                    }
                    else
                    {
                        categoryInfos.Add(item.it);
                    }
                }
            }
            return categoryInfos;
        }
        private class CategoryEqualityComparer : IEqualityComparer<CategoryInfo>
        {
            public bool Equals([AllowNull] CategoryInfo x, [AllowNull] CategoryInfo y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode([DisallowNull] CategoryInfo obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}