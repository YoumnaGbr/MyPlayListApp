using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyPlayListApp.Data.Entities;

namespace MyPlayListApp.Data.Helpers
{
    public class ImageSettings
    {
        public static string UploadImage(IFormFile image)
        {
            string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"); ;
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            string fileName = $"{Guid.NewGuid()}{image.FileName}";
            string savePath = Path.Combine(imageDirectory, fileName);

            using var fillestream = new FileStream(savePath, FileMode.Create);
            image.CopyTo(fillestream);
            
            return fileName;
        }

        public static bool IsValidImage(IFormFile imageFile)
        {
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();

            if (imageFile.Length <= 0 || !allowedExtensions.Contains(fileExtension))
            {
                return false;
            }
            return true;
        }
        public static void DeleteFile(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

        }

    }
}
