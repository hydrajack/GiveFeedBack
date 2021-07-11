using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.TestParameter;
using DeadBody.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

 

namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly FileTool FileTool;
       

        public TestController(FileTool fileTool)
        {
            FileTool = fileTool;
        }

       
        [HttpPost("")]
        public async Task<IActionResult> Post([FromForm] Test2 test)
        {                  
                    // 要存放的位置
                    var savePath = @$"Images/{test.name}";
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await test.formFile.CopyToAsync(stream);
                    }
            return Ok();
        }

        [HttpDelete("")]
        public ActionResult Delete([FromBody]string path)
        {
            FileTool.DeleteFile(path);
            return Ok();
        }
       
    }
}
