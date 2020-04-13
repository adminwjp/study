using Company.Domain.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Company.Api.Data
{
    public class ImgFactory
    {
        protected readonly IRepository<ImageInfo> _repository;
        protected readonly ILogger<ImgFactory> _logger;
        public ImgFactory(IRepository<ImageInfo> repository, ILogger<ImgFactory> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
        public ImageInfo Get(string id)
        {
            int.TryParse(id, out int iid);
            ImageInfo image = this._repository.FindSingle(it => it.Id == iid || it.Name == id || it.Href == id
              || it.Src == id);
            return image;
        }
    }
}
