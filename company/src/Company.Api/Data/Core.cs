using System;
using System.IO;

namespace Company.Api.Data
{
    public class Core
    {
        static Core()
        {
            foreach (var item in new string[] {Upload,UploadImg,UploadBrand, UploadBackgroundImage, UploadTestimonial,UploadService,UploadWork,UploadTeam })
            {
                Create(item);
            }
        }
        public static readonly string UploadDirectory = Environment.CurrentDirectory;
        private static void Create(string file)
        {
            string path = $"{UploadDirectory}\\{file}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public const string Testimonial = "testimonial";
        public const string Service = "service";
        public const string Brand = "brand";
        public const string Image = "image";
        public const string Bg = "bg";
        public const string Work = "work";
        public const string Main = "main";
        public const string Team = "team";
        //iis docker  发布时一起 要么路劲要改
        public static readonly string Upload = "wwwroot/upload";
        public static readonly string UploadImg = "wwwroot/upload/imgs";
        public static readonly string UploadTeam = "wwwroot/upload/imgs/teams";
        public static readonly string UploadBrand = "wwwroot/upload/imgs/brands";
        public static readonly string UploadBackgroundImage = "wwwroot/upload/imgs/bg";
        public static readonly string UploadTestimonial = "wwwroot/upload/imgs/testimonials";
        public static readonly string UploadService = "wwwroot/upload/imgs/services";
        public static readonly string UploadWork = "wwwroot/upload/imgs/works";
        public static readonly string UploadMain = "wwwroot/upload/imgs/mains";
    }
}
