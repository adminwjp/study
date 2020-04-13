using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Data
{
    public class AddressConfig
    {
        public static int  ImgId=1;
        private static readonly  string uploadDirectory = $"{Environment.CurrentDirectory}\\wwwroot\\upload";
        public static readonly string UploadImgDirectory = $"{uploadDirectory}\\imgs";
        public static readonly string UploadVideoDirectory = $"{uploadDirectory}\\videos";
        public static readonly string UploadMusicDirectory = $"{uploadDirectory}\\musics";
        public static readonly string UploadWordDirectory = $"{uploadDirectory}\\words";
        public static readonly string UploadExcelDirectory = $"{uploadDirectory}\\excels";
        public static readonly string UploadCsvDirectory = $"{uploadDirectory}\\csvs";
        public static readonly string UploadPdfDirectory = $"{uploadDirectory}\\pdfs";
        public static readonly string UploadPluginDirectory = $"{uploadDirectory}\\plugin";
        static AddressConfig()
        {
            CreateDirectories(new string[] { uploadDirectory , UploadImgDirectory , UploadVideoDirectory,
            UploadMusicDirectory,UploadWordDirectory,UploadExcelDirectory,UploadCsvDirectory,
            UploadPdfDirectory,UploadPluginDirectory});
        }
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void CreateDirectories(string[] paths)
        {
            foreach (var item in paths)
            {
                CreateDirectory(item);
            }
        }
    }
}
