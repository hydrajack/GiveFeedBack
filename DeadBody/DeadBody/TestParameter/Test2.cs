using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DeadBody.TestParameter
{
    public class Test2
    {
        public string name { get; set; }

        public IFormFile formFile { get; set; }


    }
}
