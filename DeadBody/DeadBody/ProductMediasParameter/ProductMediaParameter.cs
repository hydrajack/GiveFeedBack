using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.ProductMediasParameter
{
    public class ProductMediaParameter
    {
      
        public IFormFile FormFileOne { get; set; }
        public string ProductMediaVideoPath { get; set; }
        public IFormFile FormFileTwo { get; set; }
        public IFormFile FormFileThree { get; set; }
        public IFormFile FormFileFour { get; set; }
        public IFormFile FormFileFive { get; set; }
    }
}
