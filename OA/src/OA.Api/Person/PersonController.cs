using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace OA.Api.Authority
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class PersonController : BaseController<PersonInfo>
    {
        public PersonController(ILogger<PersonController> logger, IRepository<PersonInfo> repository) : base(logger, repository)
        {

        }
        protected override ResponseApi Edited(PersonInfo obj)
        {
            this.Repository.Update(it => it.Id == obj.Id, it => new PersonInfo() { UpdateDate = DateTime.Now, Address=obj.Address, ComputerGrate=obj.ComputerGrate, Email=obj.Email,
             GraduateDate=obj.GraduateDate, GraduateSchool=obj.GraduateSchool, Handset=obj.Handset, Likes=obj.Likes, OnesStrongSuit=obj.OnesStrongSuit, PartyMemberDate=obj.PartyMemberDate,
             Postlacode=obj.Postlacode, QQ=obj.QQ, SecondSchoolAge= obj.SecondSchoolAge, SecondSpeciaity=obj.SecondSpeciaity, Telphone= obj.Telphone, User=obj.User});
            return ResponseApiUtils.Success();
        }
    }
}