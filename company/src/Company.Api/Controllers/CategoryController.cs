using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Domain;
using Company.Domain.Core;
using Company.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using Utility.Ef.Repositories;
using Utility.Enums;
using Utility.Response;

namespace Company.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        protected readonly IRepository<CategoryInfo> _repository;
        protected readonly ILogger<CategoryController> _logger;
        protected CompanyDbContext CompanyDbContext;
        public CategoryController(IRepository<CategoryInfo> repository, ILogger<CategoryController> logger)
        {
            this._repository = repository;
            this._logger = logger;
            CompanyDbContext=(((BaseEfRepository<CategoryInfo>)_repository).DbContext as Company.Domain.CompanyDbContext);
        }
        [HttpGet("testimonials")]
        public IActionResult Testimonial()
        {
            var context = CompanyDbContext;
            var data = context.TestimonialPersons.Where(it=>it.Enable.HasValue && it.Enable.Value).Include(it=>it.PersonPic).Join(context.Categories.Where(it=>it.Enable.HasValue && it.Enable.Value),it=>it.Category.Id,it=>it.Id,(it,it1)=>new
            {
                CategoryInfo = (CategoryInfo)it1.Clone(),
                it = (TestimonialPersonInfo)it.Clone()
            })
            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.Testimonials = new List<TestimonialPersonInfo>();
                }
                it.Testimonials.Add(item.it);
            }
            var respone = ResponseApi.Create(Language.Chinese,Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return new JsonResult(respone);
        }
        [HttpGet("services")]
        public IActionResult Service()
        {
            var context = CompanyDbContext;
            var data = context.Services.Where(it=>it.Enable.HasValue&&it.Enable.Value).Include(it=>it.Img).Join(context.Categories.Where(it => it.Enable.HasValue && it.Enable.Value), it => it.Category.Id, it => it.Id, (it, it1) => new
            {
                CategoryInfo = (CategoryInfo)it1.Clone(),
                it = new ServiceInfo()
                {
                    Name = it.Name,
                    EnglishName = it.EnglishName,
                    Description = it.Description,
                    EnglishDescription = it.EnglishDescription,
                    Src = it.Img.Name,
                    Category = it1
                }
            })
            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.Services = new List<ServiceInfo>();
                }
                it.Services.Add(item.it);
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return new JsonResult(respone);
        }

        [HttpGet("skills")]
        public IActionResult Skill()
        {
            var context = CompanyDbContext;
            var data = context.Skills.Where(it => it.Enable.HasValue && it.Enable.Value).Join(context.Categories.Where(it => it.Enable.HasValue && it.Enable.Value).Include(it=>it.BackgroundImage), it => it.Category.Id, it => it.Id, (it, it1) => new
            {
                CategoryInfo = (CategoryInfo)it1.Clone(),
                it = new SkillInfo()
                {
                    Name = it.Name,
                    EnglishName = it.EnglishName,
                    Process = it.Process,
                    Style = it.Style,
                    Category = it1
                }
            })
            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.Skills = new List<SkillInfo>();
                }
                it.Skills.Add(item.it);
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return new JsonResult(respone);
        }
        [HttpGet("themes")]
        public IActionResult Theme()
        {
            var context = CompanyDbContext;
            var data = context.Themes.Where(it => it.Enable.HasValue && it.Enable.Value).Join(context.Categories.Where(it => it.Enable.HasValue && it.Enable.Value), it => it.Category.Id, it => it.Id, (it, it1) => new
            {
                CategoryInfo = (CategoryInfo)it1.Clone(),
                it = new ThemeInfo()
                {
                    Name = it.Name,
                    EnglishName = it.EnglishName,
                    Href=it.Href,
                    Category = it1
                }
            })
            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.Themes = new List<ThemeInfo>();
                }
                it.Themes.Add(item.it);
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp;
            return new JsonResult(respone);
        }
        [HttpGet("partners")]
        public IActionResult Partners()
        {
            var context = CompanyDbContext;
            var data = context.Brands.Where(it => it.Enable.HasValue && it.Enable.Value && it.Feature==null).Include(it=>it.Logo)
                .Join(context.Categories.Where(it => it.Enable.HasValue && it.Enable.Value).Include(it=>it.BackgroundImage), it => it.Category.Id, it => it.Id, (it, it1) => new
            {
                CategoryInfo = (CategoryInfo)it1.Clone(),
                it = new BrandInfo()
                {
                    Href = it.Href,
                    Src=it.Logo.Name,
                    Category = it1
                }
            })
            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.OurPartners = new List<BrandInfo>();
                }
                it.OurPartners.Add(item.it);
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return new JsonResult(respone);
        }
        [HttpGet("works")]
        public IActionResult Works()
        {
            var context = CompanyDbContext;
            var data = context.Works.Where(it => it.Enable.HasValue && it.Enable.Value).Include(it => it.Image)
                .Join(context.Categories.Where(it => it.Enable.HasValue && it.Enable.Value), it => it.Category.Id, it => it.Id, (it, it1) => new
                {
                    CategoryInfo = (CategoryInfo)it1.Clone(),
                    it = new WorkInfo()
                    {
                        Src = it.Image.Name
                    }
                })
            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.Works = new List<WorkInfo>();
                }
                it.Works.Add(item.it);
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return new JsonResult(respone);
        }
        [HttpGet("features")]
        public IActionResult Features()
        {
            var context = CompanyDbContext;
            var data = context.Brands.Where(it => it.Enable.HasValue && it.Enable.Value && it.Feature!=null)
                .Join(context.Categories.Where(it => it.Enable.HasValue && it.Enable.Value), it => it.Category.Id, it => it.Id, (it, it1) => new
                {
                    CategoryInfo = (CategoryInfo)it1.Clone(),
                    it = new BrandInfo()
                    {
                        Name=it.Name,
                        EnglishName=it.EnglishName,
                        Description=it.Description,
                        EnglishDescription=it.EnglishDescription,
                        Feature=it.Feature
                    }
                })
            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.Features = new List<BrandInfo>();
                }
                it.Features.Add(item.it);
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = temp.FirstOrDefault();
            return new JsonResult(respone);
        }
        [HttpGet("teams")]
        public IActionResult Teams()
        {
            var context = CompanyDbContext;
            var data = context.Teams.Where(it => it.Enable.HasValue && it.Enable.Value)
                .Join(context.Categories.Where(it => it.Enable.HasValue && it.Enable.Value), it => it.Category.Id, it => it.Id, (it, it1) => new
                {
                    CategoryInfo = (CategoryInfo)it1.Clone(),
                    it
                }).Join(context.Services, it => it.it.Service.Id, it => it.Id, (it, it1) => new {  it.CategoryInfo, TeamInfo = it.it, ServiceInfo = new ServiceInfo()
                {
                    Name = it1.Name,
                    EnglishName = it1.EnglishName
                }
                }).Join(context.Images, it => it.TeamInfo.Img.Id, it => it.Id, (it, it1) => new {it.CategoryInfo, it.TeamInfo,  it.ServiceInfo, Src = it1.Name })
            .GroupJoin((context.TeamSourceInfos.Include(it => it.Social)), it => it.TeamInfo.Id, it => it.Team.Id, (it, it1) => new
            {
                it.CategoryInfo,
                it.TeamInfo,
                it.ServiceInfo,
                it.Src,
                it1
            }).SelectMany(it => it.it1.DefaultIfEmpty(), (it, it1) => new
            {
                it.CategoryInfo,
                it.TeamInfo,
                it.ServiceInfo,
                it.Src,
                it1.Social.Icon,
                it1.Social.Href
            })

            .ToList();
            var temp = new List<CategoryInfo>(data.Count);
            foreach (var item in data)
            {
                var it = temp.Find(it => it.Id == item.CategoryInfo.Id);
                if (it == null)
                {
                    it = item.CategoryInfo;
                    temp.Add(it);
                    it.Teams = new List<TeamInfo>();
                }
                var exists = it.Teams.ToList().Find(it => it.Id == item.TeamInfo.Id);
                if (exists!=null)
                {
                    exists.Source += "," + item.Icon; 
                    exists.Href += "," + item.Href;
                    continue;
                }
                
                item.TeamInfo.Category = item.CategoryInfo;
                item.TeamInfo.Service = item.ServiceInfo;
                item.TeamInfo.Src = item.Src;
                item.TeamInfo.Source = item.Icon; 
                item.TeamInfo.Href = item.Href;
                it.Teams.Add(item.TeamInfo);
            }
            var firstData = temp.FirstOrDefault();
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data = firstData;
            return new JsonResult(respone);
        }
        [HttpGet("porworks")]
        public IActionResult PorWorks()
        {
            var context = CompanyDbContext;
            var data = context.WorkCategories.Include(it=>it.Parent)
                .Join(context.Works.Include(it => it.Image).Include(it => it.Category), it => it.Work.Id, it => it.Id, (it, it1) => new WorkCategoryInfo() { Id = it.Id, Parent = it.Parent, Work = it1 })
                //.GroupJoin(context.Works.Include(it=>it.Image).Include(it=>it.Category),it=>it.Work.Id,it=>it.Id,(it,it1)=> new { it,it1})
                //.SelectMany(it=>it.it1.DefaultIfEmpty(),(it,it1)=> new WorkCategoryInfo() { Id = it.it.Id, Parent = it.it.Parent, Work = it1 })
                .GroupJoin(context.Categories,it=>it.Work.Category.Id,it=>it.Id,(it,it1)=> new { it,it1}
                ).SelectMany(it=>it.it1.DefaultIfEmpty(),(it,it1) =>
                new WorkCategoryInfo()
                {
                    Id = it.it.Id,
                    ParentId = it.it.Parent.Id,
                    Name = it.it.Parent.Name,
                    EnglishName = it.it.Parent.EnglishName,
                    Work=new WorkInfo() { 
                        Category= new CategoryInfo()
                        {
                            Name = it1.Name,
                            EnglishName = it1.EnglishName,
                            Description = it1.Description,
                            EnglishDescription = it1.EnglishDescription,
                        }
                    },
                    Src = it.it.Work.Image.Name,
                    Filter = it.it.Parent.Filter
                })

               .ToList();
            WorkCategoryResult workCategoryResult = new WorkCategoryResult() { Category=new CategoryResult(),WorkCategories=new List<WorkCategoryEntry>(),Works=new List<WorkResult>()};
            foreach (var item in data)
            {
                workCategoryResult.Category.Name = item.Work.Category.Name;
                workCategoryResult.Category.EnglishName = item.Work.Category.EnglishName;
                workCategoryResult.Category.Description = item.Work.Category.Description;
                workCategoryResult.Category.EnglishDescription = item.Work.Category.EnglishDescription;
                var workCategory = workCategoryResult.WorkCategories.Find(it=>it.Id==item.ParentId);
                if (workCategory == null)
                {
                    workCategory = new WorkCategoryEntry() { Id = item.ParentId, Filter = item.Filter, Name = item.Name, EnglishName = item.EnglishName
                    ,Ids=new List<int?>()};
                    workCategoryResult.WorkCategories.Add(workCategory);
                }
                //workCategory.Ids.Add(item.Id);
                var work = workCategoryResult.Works.Find(it => it.Src == item.Src);
                if (work == null)
                {
                    work = new WorkResult() { Id = item.Id, Filter = item.Filter.Trim('.'), Src=item.Src };
                    work.Filter = work.Filter == "*" ? string.Empty : work.Filter;
                    workCategoryResult.Works.Add(work);
                }
                else
                {
                    if (item.Filter.Equals("*") ||work.Filter.IndexOf(item.Filter.Trim('.'))>-1)
                    {

                    }
                    else
                    {
                        work.Filter += " " + item.Filter.Trim('.');
                    }
                }
            }
            var respone = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            respone.Data =workCategoryResult;
            return new JsonResult(respone);
        }
    }
}