using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Domain.Core;
using Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Company.Domain;
using System.Text;
using Dapper;

namespace Company.Api.Areas.Admin.Controllers
{

    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class AdminController : BaseController<AdminInfo>
    {
        public AdminController(IRepository<AdminInfo> repository, ILogger<AdminController> logger):base(repository,logger)
        {

        }
        protected override void AddMiddleExecet(AdminInfo obj)
        {
            if (obj.Role != null && obj.Role.Id.HasValue)
            {
                obj.Role = ((CompanyDbContext)base.Repository.DbContext).Roles.Find(new object[] { obj.Role.Id});
            }
        }
        protected override void EditMiddleExecet(AdminInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<AdminInfo> QueryList(AdminInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj)).Include(it=>it.Role)/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
        [HttpGet("total")]
        public  ResponseApi Total()
        {
            ResponseApi response = ResponseApiUtils.Success();
            string sql = ToSql();
			StringBuilder builder = new StringBuilder(500);

			string[] strs = new string[] {
				"about_info", "about","关于",
				"admin_info", "admin","管理员",
				"admin_role_info", "admin_role","管理员角色",
				"basic_category_info", "basic_category","基本分类",
				"category_info", "category","分类",
				"company_info", "company","公司",
				"image_info", "img","图片",
				"main_info", "main","公司主体",
				"media_info", "media","媒体",
				"menu_info", "menu","菜单",
				"nav_info", "nav","导航",
				"service_info", "service","我们的服务",
				"skill_info", "skill","我们的技能",
				"skin_info", "skin","皮肤",
				"social_info", "social","社交",
				"team_info", "team","团队",
				"team_source_info", "team_source","团队来源",
				"testimonial_person_info", "testimonial_person","感言个人",
				"theme_info", "theme","主题",
				"user_info", "user1","用户",
				"work_category_info", "work_category","作品来源",
				"work_info", "work","作品",
			};
			string[] names = new string[strs.Length / 3];
			for (int i = 0; i < strs.Length/3; i++)
			{
		
				int j = i*3;
				names[i] = strs[j + 2];
				 string sqlformat = string.Format(sql, strs[j], strs[j+1]/*,strs[j+2]*/);
				builder.Append(sqlformat);
				if(i!= names.Length - 1)
				{
					builder.Append("  UNION ALL ");
				}
			}
			string totalSql = builder.ToString();
		    var data=	(base.Repository.DbContext as Company.Domain.CompanyDbContext).Database.GetDbConnection().Query(totalSql);
			response.Data = new { data,name=names};
			return response;

        }
        private string ToSql()
		{//   '{2}' name,
			string sql = @"SELECT
	{1}.total_count,
	{1}.today_count,
	{1}.preday_count,
	{1}.week_count ,
	{1}.month_count ,
	{1}.year_count
FROM
	(
	SELECT
		count( 1 ) total_count,
		today.today_count,
		preday.preday_count,
		prewek.week_count,
		premonth.month_count,
		preyear.year_count
		
	FROM
		{0},
		( SELECT count( 1 ) today_count FROM image_info WHERE create_date BETWEEN DATE_FORMAT( CURRENT_DATE, '%Y-%m-%d 00:00:00' ) AND DATE_FORMAT( CURRENT_DATE, '%Y-%m-%d 23:59:59' ) ) today,
		(
		SELECT
			count( 1 ) preday_count 
		FROM
			{0} 
		WHERE
			create_date BETWEEN DATE_FORMAT( DATE_SUB( CURDATE( ), INTERVAL 1 DAY ), '%Y-%m-%d 00:00:00' ) 
			AND DATE_FORMAT(CURRENT_DATE, '%Y-%m-%d 23:59:59' ) 
		) preday,
		(
		SELECT
			count( 1 ) week_count 
		FROM
			{0} 
		WHERE
			create_date BETWEEN DATE_FORMAT( DATE_SUB( CURDATE( ), INTERVAL 1 WEEK ), '%Y-%m-%d 00:00:00' ) 
			AND DATE_FORMAT(CURRENT_DATE, '%Y-%m-%d 23:59:59' ) 
		) prewek,
		(
		SELECT
			count( 1 ) month_count 
		FROM
			{0} 
		WHERE
			create_date BETWEEN DATE_FORMAT( DATE_SUB( CURDATE( ), INTERVAL 1 MONTH ), '%Y-%m-%d 00:00:00' ) 
			AND DATE_FORMAT(CURRENT_DATE, '%Y-%m-%d 23:59:59' ) 
		) premonth ,
		(SELECT
			count( 1 ) year_count 
		FROM
			{0} 
		WHERE
			create_date BETWEEN DATE_FORMAT( DATE_SUB(CURDATE(), INTERVAL 1 year) ,'%Y-%m-%d 00:00:00' ) 
			AND DATE_FORMAT(CURRENT_DATE, '%Y-%m-%d 23:59:59'))
preyear
) {1} ";
			return sql;
        }
    }
}