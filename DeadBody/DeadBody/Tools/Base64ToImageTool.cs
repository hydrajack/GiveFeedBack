using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeadBody.Tools
{
    public class Base64ToImageTool
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="base64String"></param>
        /// <param name="imageCategory"></param>
        /// <param name="id">大頭貼類型:直接填id,媒體類型:填id加分支,ex:2-1,2-2,2-3,2-4,2-5</param>
        /// <returns></returns>
        public string LoadImageJpg(string imagePath, string base64String)
        {                
            string fileName = $"{imagePath}.jpg";

            byte[] bytes = Convert.FromBase64String($@"{ base64String}");
            // Convert byte[] to Image
            using (var ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                image.Save(fileName, ImageFormat.Jpeg);
                return  fileName;
            }

        }

        
 
        public void DeleteImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
     
    }
}
