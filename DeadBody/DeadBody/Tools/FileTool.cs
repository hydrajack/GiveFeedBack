using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.Tools
{
    public class FileTool
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="base64String"></param>
        /// <param name="imageCategory"></param>
        /// <param name="id">大頭貼類型:直接填id,媒體類型:填id加分支,ex:2-1,2-2,2-3,2-4,2-5</param>
        /// <returns></returns>
        public async  void  SaveFormFile(string imagePath, IFormFile formFile)
        {
            // 要存放的位置ex:
            //var savePath = @$"Images/{test.name}";
            var savePath = @$"{imagePath}";
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);

            }
            
        }


        public void DeleteFile(string imagePath)
        {
            if (File.Exists($@"{imagePath}"))
            {
                File.Delete($@"{imagePath}");
            }

        }

        public string SaveImageFile(IFormFile formFile,string folder,int id,int index)
        {
            string[] typeSpilt = formFile.FileName.Split(".");
            string[] typeSpilt2 = formFile.ContentType.Split("/");
            string path = $@"Images/{folder}/{typeSpilt[0]}{id}-{index}.{typeSpilt2[1]}";
            SaveFormFile(path, formFile);
            return path;
        }
        
           
         
     
    }
}
