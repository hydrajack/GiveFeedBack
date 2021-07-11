using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.ProductHistoriesParameter
{
    public class ProductHistoryPut
    {
        public string ProductHistoryTitle { get; set; }
        public IFormFile HeadShotFile { get; set; }
        public string ProductHistoryContent { get; set; }
        public string ProductHistoryVersion { get; set; }
    }
}
