using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.ProductHistoryMediasParameter
{
    public class ProductHistoryMediaParameter
    {
        public IFormFile FormFileOne { get; set; }
        public string ProductHistoryMediaVideoPath { get; set; }
        public IFormFile FormFileTwo { get; set; }
        public IFormFile FormFileThree { get; set; }
        public IFormFile FormFileFour { get; set; }
        public IFormFile FormFileFive { get; set; }
    }
}
