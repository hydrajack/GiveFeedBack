using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.ProductHistoriesParameter
{
    public class ProductHistoryPost
    {
        public int ProductId { get; set; }
        public string ProductHistoryTitle { get; set; }
        public IFormFile HeadShotFile { get; set; }
        public int ProductHistoryMediaId { get; set; }
        public string ProductHistoryContent { get; set; }
        public string ProductHistoryVersion { get; set; }
    }
}
