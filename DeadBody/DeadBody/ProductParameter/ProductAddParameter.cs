using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.ProductParameter
{
    //[frombody]{產品建立帳號流水號 、標題、副標、產品介紹內文、產品大頭貼網址、產品上傳網址、產品展示媒體編號、產品分類}
    public class ProductAddParameter
    {
        public int UserId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSubtitle { get; set; }
        public string ProductContent { get; set; }
        public IFormFile HeadShotFile { get; set; }
        public string ProductUrl { get; set; }
        public int ProductMediaId { get; set; }//
        public string ProductCategory { get; set; }


    }
}
