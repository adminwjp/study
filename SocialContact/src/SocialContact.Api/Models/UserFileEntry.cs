using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Models
{
    public class UserFileEntry
    {
        public string AbstractUrl { get; set; }
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileSrc { get; set; }
        public int UserFileId { get; set; }
        public string UserFileName { get; set; }
        public string UserFileSrc { get; set; }
        public bool FileFlag { get; set; }
    }
}
