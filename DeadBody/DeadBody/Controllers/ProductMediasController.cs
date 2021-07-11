using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Models.db;
using DeadBody.ProductMediasParameter;
using DeadBody.Tools;
using Microsoft.AspNetCore.Mvc;

 

namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMediasController : ControllerBase
    {
        private readonly BackEndContext Context;
        private readonly FileTool FileTool;

        public ProductMediasController(BackEndContext context, FileTool fileTool)
        {
            Context = context;
            FileTool = fileTool;
        }
        /// <summary>
        /// 透過產品展示媒體編號找到相對應的媒體
        /// </summary>
        /// <param name="productMediaId"></param>
        /// <returns></returns>
        [HttpGet("{productMediaId}")]
        public ActionResult  GetProductMedia(int productMediaId)
        {
            var productMedia = Context.ProductMedias.Where(tuples => tuples.ProductMediaId == productMediaId).SingleOrDefault();
            if(productMedia is null)
            {
                return NotFound("無法用產品展示媒體編號找到相對應的媒體");
            }
            return Ok(productMedia);
            
        }

        [HttpPost("")]

        public ActionResult PostProductMedia([FromForm]ProductMediaParameter parameter)
        {
            var newProductMedia = new ProductMedia {
                ProductMediaVideoPath = parameter.ProductMediaVideoPath
            };
            Context.ProductMedias.Add(newProductMedia);
            Context.SaveChanges();           
       
            if(parameter.FormFileOne is not null)
            {                                        
                newProductMedia.ProductMediaPathOne = FileTool.SaveImageFile(parameter.FormFileOne, "ProductMedia"
                    , newProductMedia.ProductMediaId, 1);
            }
            if(parameter.FormFileTwo is not null)
            {           
                newProductMedia.ProductMediaPathTwo = FileTool.SaveImageFile(parameter.FormFileTwo, "ProductMedia"
                    , newProductMedia.ProductMediaId, 2);
            }
            if(parameter.FormFileThree is not null)
            {            
                newProductMedia.ProductMediaPathThree = FileTool.SaveImageFile(parameter.FormFileThree, "ProductMedia"
                    , newProductMedia.ProductMediaId, 3);
            }
            if(parameter.FormFileFour is not null)
            {                
                newProductMedia.ProductMediaPathFour = FileTool.SaveImageFile(parameter.FormFileFour, "ProductMedia"
                    , newProductMedia.ProductMediaId, 4);
            }
             if(parameter.FormFileFive is not null)
            {            
                newProductMedia.ProductMediaPathFive = FileTool.SaveImageFile(parameter.FormFileFive, "ProductMedia"
                    , newProductMedia.ProductMediaId, 5);
            }       
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetProductMedia), new { productMediaId = newProductMedia.ProductMediaId }, newProductMedia);
        }

         
        [HttpPut("{productMediaId}")]
        public ActionResult PutProductMedia(int productMediaId,[FromForm] ProductMediaParameter parameter)
        {
            var productMedia = Context.ProductMedias.Where(tuples => tuples.ProductMediaId == productMediaId).SingleOrDefault();
           
            if(productMedia is null)
            {
                return NotFound("無法用產品展示媒體編號找到相對應的媒體");
            }
           
            
            productMedia.ProductMediaVideoPath = parameter.ProductMediaVideoPath;
             
            //string path = @$"Images/ProductMedia/productMedia{productMediaId}";

            if (parameter.FormFileOne is not null)
            {
                FileTool.DeleteFile(productMedia.ProductMediaPathOne);               
                productMedia.ProductMediaPathOne = FileTool.SaveImageFile(parameter.FormFileOne, "ProductMedia"
                    , productMedia.ProductMediaId, 1);

            }
            if (parameter.FormFileTwo is not null)
            {
                FileTool.DeleteFile(productMedia.ProductMediaPathTwo);              
                productMedia.ProductMediaPathTwo = FileTool.SaveImageFile(parameter.FormFileTwo, "ProductMedia"
                    , productMedia.ProductMediaId, 2);
            }
            if (parameter.FormFileThree is not null)
            {
                FileTool.DeleteFile(productMedia.ProductMediaPathThree);
                
                productMedia.ProductMediaPathThree = FileTool.SaveImageFile(parameter.FormFileThree, "ProductMedia"
                    , productMedia.ProductMediaId, 3);

            }
            if (parameter.FormFileFour is not null)
            {            
                productMedia.ProductMediaPathFour = FileTool.SaveImageFile(parameter.FormFileFour, "ProductMedia"
                    , productMedia.ProductMediaId,4);
            }
            if (parameter.FormFileFive is not null)
            {
                FileTool.DeleteFile(productMedia.ProductMediaPathFive);               
                productMedia.ProductMediaPathFive = FileTool.SaveImageFile(parameter.FormFileFive, "ProductMedia"
                    , productMedia.ProductMediaId, 5);

            }
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetProductMedia), new { productMediaId = productMedia.ProductMediaId }, productMedia);
        }

        [HttpPut("{productMediaId}/delete")]
        public ActionResult DeleteByIndex(int productMediaId,[FromBody] ProductMediaDelete parameter)
        {
            var productMedia = Context.ProductMedias.Where(tuples => tuples.ProductMediaId == productMediaId).SingleOrDefault();
            if(productMedia is null)
            {
                return NotFound();
            }
            switch (parameter.Index)
            {
                case 1:
                    FileTool.DeleteFile(productMedia.ProductMediaPathOne);
                    productMedia.ProductMediaPathOne = null;
                    break;
                case 2:
                    FileTool.DeleteFile(productMedia.ProductMediaPathTwo);
                    productMedia.ProductMediaPathTwo = null;
                    break;
                case 3:
                    FileTool.DeleteFile(productMedia.ProductMediaPathThree);
                    productMedia.ProductMediaPathThree = null;
                    break;
                case 4:
                    FileTool.DeleteFile(productMedia.ProductMediaPathFour);
                    productMedia.ProductMediaPathFour = null;
                    break;
                case 5:
                    FileTool.DeleteFile(productMedia.ProductMediaPathFive);
                    productMedia.ProductMediaPathFive = null;
                    break;
            }

            Context.SaveChanges();
            return CreatedAtAction(nameof(GetProductMedia), new { productMediaId = productMedia.ProductMediaId }, productMedia);
        }



    }
}
