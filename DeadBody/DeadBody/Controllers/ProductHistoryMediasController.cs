using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Models.db;
using DeadBody.ProductHistoryMediasParameter;
using DeadBody.ProductMediasParameter;
using DeadBody.Tools;
using Microsoft.AspNetCore.Mvc;
 
namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductHistoryMediasController : ControllerBase
    {
        private readonly FileTool FileTool;
        private readonly BackEndContext BackEndContext;

        public ProductHistoryMediasController(BackEndContext backEndContext,FileTool fileTool)
        {
            FileTool = fileTool;
            BackEndContext = backEndContext;

        }
        [HttpPost("")]
        public ActionResult PostProductHistoryMedia([FromForm] ProductHistoryMediaParameter parameter)
        {
            var historyMedia = new ProductHistoryMedia {
                ProductHistoryMediaVideoPath = parameter.ProductHistoryMediaVideoPath
            };
            BackEndContext.ProductHistoryMedias.Add(historyMedia);
            BackEndContext.SaveChanges();
                     
            if (parameter.FormFileOne is not null)
            {             
                historyMedia.ProductHistoryMediaPathOne = FileTool.SaveImageFile(parameter.FormFileOne, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 1);
            }
            if (parameter.FormFileTwo is not null)
            {             
                historyMedia.ProductHistoryMediaPathTwo = FileTool.SaveImageFile(parameter.FormFileTwo, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 2);
            }
            if (parameter.FormFileThree is not null)
            {            
                historyMedia.ProductHistoryMediaPathThree = FileTool.SaveImageFile(parameter.FormFileThree, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 3);
            }
            if (parameter.FormFileFour is not null)
            {               
                historyMedia.ProductHistoryMediaPathFour = FileTool.SaveImageFile(parameter.FormFileFour, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 4);
            }
            if (parameter.FormFileFive is not null)
            {               
                historyMedia.ProductHistoryMediaPathFive = FileTool.SaveImageFile(parameter.FormFileFive, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 5);
            }
            BackEndContext.SaveChanges();
            return CreatedAtAction(nameof(GetProductHistoryMedia), new { productHistoryMediaId = historyMedia.ProductHistoryMediaId }, historyMedia);

        }
        
         
        [HttpGet("{productHistoryMediaId}")]
        public ActionResult GetProductHistoryMedia(int productHistoryMediaId)
        {
            var historyMedia = BackEndContext.ProductHistoryMedias.Where(tuples => tuples.ProductHistoryMediaId == productHistoryMediaId);
            if(historyMedia is null)
            {
                return NotFound("無法用產品展示頁媒體編號取得產品歷程媒體");
            }
            return Ok(historyMedia);
        }

        [HttpPut("{productHistoryMediaId}")]
        public ActionResult PutProductHistoryMedia(int productHistoryMediaId,[FromForm]ProductHistoryMediaParameter parameter)
        {
            var historyMedia = BackEndContext.ProductHistoryMedias.Where(tuples => tuples.ProductHistoryMediaId == productHistoryMediaId).SingleOrDefault();
            string imagPath= @$"Images/ProductHistoryMedia/productHistoryMedia{productHistoryMediaId}";
            if (historyMedia is null)
            {
                return NotFound("無法用產品展示媒體編號找到相對應的媒體");
            }

            historyMedia.ProductHistoryMediaVideoPath = parameter.ProductHistoryMediaVideoPath;
              
             
            if (parameter.FormFileOne is not null)
            {
                FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathOne);           
                historyMedia.ProductHistoryMediaPathOne = FileTool.SaveImageFile(parameter.FormFileOne, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 1);

            }
            if (parameter.FormFileTwo is not null)
            {
                FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathTwo);                
                historyMedia.ProductHistoryMediaPathTwo = FileTool.SaveImageFile(parameter.FormFileTwo, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 2);

            }
            if (parameter.FormFileThree is not null)
            {
                FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathThree);
                historyMedia.ProductHistoryMediaPathThree = FileTool.SaveImageFile(parameter.FormFileThree, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 3);

            }
            if (parameter.FormFileFour is not null)
            {
                FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathFour);              
                historyMedia.ProductHistoryMediaPathFour = FileTool.SaveImageFile(parameter.FormFileFour, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 4);

            }
            if (parameter.FormFileFive is not null)
            {
                FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathFive);                
                historyMedia.ProductHistoryMediaPathFive = FileTool.SaveImageFile(parameter.FormFileFive, "ProductHistoryMedia"
                    , historyMedia.ProductHistoryMediaId, 5);
            }
            BackEndContext.SaveChanges();
            return CreatedAtAction(nameof(GetProductHistoryMedia), new { productHistoryMediaId = historyMedia.ProductHistoryMediaId }, historyMedia);
        }

        [HttpPut("{productHistoryMediaId}/delete")]
        public ActionResult DeleteByIndex(int productHistoryMediaId, [FromBody] ProductMediaDelete parameter)
        {
            var historyMedia = BackEndContext.ProductHistoryMedias.Where(tuples => 
            tuples.ProductHistoryMediaId == productHistoryMediaId).SingleOrDefault();

            if (historyMedia is null)
            {
                return NotFound();
            }
            switch (parameter.Index)
            {
                case 1:
                    FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathOne);
                    historyMedia.ProductHistoryMediaPathOne = null;
                    break;
                case 2:
                    FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathTwo);
                    historyMedia.ProductHistoryMediaPathTwo = null;
                    break;
                case 3:
                    FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathThree);
                    historyMedia.ProductHistoryMediaPathThree = null;
                    break;
                case 4:
                    FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathFour);
                    historyMedia.ProductHistoryMediaPathFour = null;
                    break;
               case 5:
                    FileTool.DeleteFile(historyMedia.ProductHistoryMediaPathFive);
                    historyMedia.ProductHistoryMediaPathFive = null;
                    break;                            
            }           
            BackEndContext.SaveChanges();
            return CreatedAtAction(nameof(GetProductHistoryMedia), new { productHistoryMediaId = historyMedia.ProductHistoryMediaId }, historyMedia);
        }


    }
}
