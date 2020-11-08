using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using System.Xml.Serialization;
using Utility.Domain.Repositories;
using Utility.Response;

namespace OA.Api.AccountItem
{
	[Route("api/v{version:apiVersion}/account_item")]
	[ApiController]
	[ApiVersion("1.0")]
	[ProducesResponseType(typeof(ResponseApi), 200)]
	public class AccountItemController : BaseController<AccountItemInfo>
	{
		public AccountItemController(ILogger<AccountItemController> logger, IRepository<AccountItemInfo> repository) : base(logger, repository)
		{

		}
		protected override ResponseApi Edited(AccountItemInfo obj)
		{
			base.Repository.Update(it => it.Id == obj.Id, it => new AccountItemInfo() { Name = obj.Name, Type = obj.Type, Utit = obj.Utit, UpdateDate = DateTime.Now });
			return ResponseApi.CreateSuccess();
		}
		[HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Find(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
	}
}