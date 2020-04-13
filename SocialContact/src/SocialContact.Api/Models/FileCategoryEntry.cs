using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Models
{
    public class FileCategoryEntry
    {
        private readonly HashSet<string> _sufixs = new HashSet<string>();
        string _accept;
        public int Id { get; set; }
        public string Category { get; set; }
        public string Accept { get => this._accept; 
            set {
                this._sufixs.Clear();
                if (!string.IsNullOrEmpty(value))
                {
                    foreach (var item in value.Split(','))
                    {
                        this._sufixs.Add(item);
                    }
                }
                this._accept = value;
            }
        }
        public HashSet<string> Sufixs
        {
            get {
                return this._sufixs;
            }
        } 
    }
}
