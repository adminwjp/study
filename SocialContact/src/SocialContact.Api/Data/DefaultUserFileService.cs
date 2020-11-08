using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.Domain.Uow;

namespace SocialContact.Api.Data
{
    public class DefaultUserFileService : IUserFileService
    {
        public DefaultUserFileService(IUnitWork unitWork, IMemoryCache cache, Core core, ILogger<DefaultUserFileService> logger)
        {
            this.UnitWork = unitWork;
            this.Cache = cache;
            this.Core = core;
            this.Logger = logger;
        }
        public ILogger<IUserFileService> Logger { get; }

        public IUnitWork UnitWork { get; }

        public IMemoryCache Cache { get; }

        public Core Core { get; }
    }
}

