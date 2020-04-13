using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.ViewModel
{
    public class WorkCategoryResult
    {
        public List<WorkCategoryEntry> WorkCategories { get; set; }
        public List<WorkResult> Works { get; set; }
        public CategoryResult Category { get; set; }
    }
    public class WorkCategoryEntry{
        [Newtonsoft.Json.JsonIgnore]
        public int? Id { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public List<int?> Ids { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Filter { get; set; }
    }
    public class WorkResult
    {
        [Newtonsoft.Json.JsonIgnore]
        public int? Id { get; set; }
        public string Src { get; set; }
        public string Filter { get; set; }
    }
    public class CategoryResult
    {
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
    }
}
