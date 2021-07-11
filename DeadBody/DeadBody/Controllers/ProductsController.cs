using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Models.db;
using DeadBody.ProductParameter;
using DeadBody.Tools;
using Microsoft.AspNetCore.Mvc;

 
namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly BackEndContext Context;
        private readonly FileTool FileTool;
        public ProductsController(BackEndContext context, FileTool fileTool)
        {
            Context = context;
            FileTool = fileTool;
        }
        /// <summary>
        /// 取得單筆產品展示頁面資料
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public ActionResult GetProductByProductId(int productId)
        {
            var product = Context.Products.Where(tuples => tuples.ProductId == productId).SingleOrDefault();
            if(product is null)
            {
                return NotFound("無法用產品建立編號找到產品，請確認輸入是否正確");
            }         
            return Ok(product);
        }
        /// <summary>
        /// 取得符合搜尋 的所有產品展示頁面
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("search/{keyword}")]
        public ActionResult GetProductsBySearch(string keyword)
        {
            var products = Context.Products.Where(tuples => tuples.ProductTitle.Contains(keyword));

            if(products.Count()<=0)
            {
                return NotFound("無法用關鍵字查到任何產品標題");
            }
            return Ok(products);
        }
        /// <summary>
        /// 取得符合分類 的所有產品展示頁面
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("category/{category}")]
        public ActionResult GetProductsByCategory(string category)
        {
            var products = Context.Products.Where(tuples => tuples.ProductCategory.Equals(category));

            if (products.Count()<=0)
            {
                return NotFound("無法用輸入的分類查到任何相關分類");
            }
            return Ok(products);
        }
        /// <summary>
        /// 取得符合產品建立帳號的所有產品展示頁面
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("users/{userId}")]
        public ActionResult GetProductsByUserId(int userId)
        {
            var products = Context.Products.Where(tuples => tuples.UserId==userId);

            if (products is null)
            {
                return NotFound("查不到該會員有關任何發表之產品展示，或者沒有此會員");
            }
            return Ok(products);
        }
        /// <summary>
        /// 取得所有產品展示頁面資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult GetAllProducts()
        {
            var products = Context.Products;

            if(products is null)
            {
                return NotFound("沒有任何一個產品發表存在");
            }
            return Ok(products);

        }
        
        
        
        //public string ProductUrl { get; set; }
        [HttpPut("{productId}")]
        public ActionResult PutProductByProductId(int productId,[FromForm]ProductPutParameter parameter)
        {
            var product = Context.Products.Where(tuples => tuples.ProductId == productId).SingleOrDefault();
            if(product is null)
            {
                return NotFound("無法用產品建立編號找到產品，請確認輸入是否正確");
            }
            if(parameter.ProductTitle is not null)
            {
                product.ProductTitle = parameter.ProductTitle;
            }
            if(parameter.ProductSubtitle is not null)
            {
                product.ProductSubtitle = parameter.ProductSubtitle;
            }
            if(parameter.ProductContent is not null)
            {
                product.ProductContent = parameter.ProductContent;
            }
            if (parameter.HeadShotFile is not null)
            {
                FileTool.DeleteFile(product.ProductHeadShotPath);
                
                product.ProductHeadShotPath = FileTool.SaveImageFile(parameter.HeadShotFile, "ProductHeadShot", product.ProductId, 0);

            }         
            if(parameter.ProductUrl is not null)
            {
                product.ProductUrl = parameter.ProductUrl;
            }
            if(parameter.ProductCategory is not null)
            {
                product.ProductCategory = parameter.ProductCategory;
            }
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetProductByProductId), new { productId = product.ProductId }, product);

        }
        /// <summary>
        /// 新增單筆產品展示頁面資料
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult Post([FromForm] ProductAddParameter parameter)
        {
            var user = Context.Users.Where(tuples => tuples.UserId == parameter.UserId).SingleOrDefault();
            if(user is null)
            {
                return NotFound();
            }
            var productMedia = Context.ProductMedias.Where(tuples => tuples.ProductMediaId == parameter.ProductMediaId).SingleOrDefault();
            if(productMedia is null)
            {
                return NotFound();
            }
            //ProductHeadShotPath改成可以null,先不放Path
            var newProduct = new Product
            {
                UserId = parameter.UserId,
                ProductTitle = parameter.ProductTitle,
                ProductSubtitle = parameter.ProductSubtitle,
                ProductContent = parameter.ProductContent,
                //ProductHeadShotPath = productHeadShotPath,
                ProductUrl = parameter.ProductUrl,
                ProductMediaId = parameter.ProductMediaId,//要有這個才知道對應哪個媒體編號
                ProductCategory = parameter.ProductCategory
            };          
            Context.Products.Add(newProduct);
           
            //根據流水號當作命名
            if(parameter.HeadShotFile is not null)
            {              
                newProduct.ProductHeadShotPath = FileTool.SaveImageFile(parameter.HeadShotFile, "ProductHeadShot", newProduct.ProductId, 0);
            }
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetProductByProductId), new { productId = newProduct.ProductId}, newProduct);
        }
        [HttpDelete("{productId}/HeadShot")]
        public ActionResult DeleteUserHeadShot(int productId)
        {
            var product = Context.Products.Where(tuples => tuples.ProductId == productId).SingleOrDefault();
            if (product is null)
            {
                return NotFound("查無相關產品流水號");
            }
            if (product.ProductHeadShotPath is null)
            {
                return NotFound("此產品沒有存妳想刪的照片");
            }
            FileTool.DeleteFile(product.ProductHeadShotPath);
            var InfoColumn = product.GetType().GetProperty("ProductHeadShotPath");
            InfoColumn.SetValue(product, null);
            Context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            var product = Context.Products.Where(tuples => tuples.ProductId == productId).SingleOrDefault();
            if(product is null)
            {
                return NotFound("無法用產品建立編號找到產品");
            }
            //刪產品追蹤
            var productFollowers = Context.ProductFollowers.Where(tuples => tuples.FollowedProductId == productId);
            Context.ProductFollowers.RemoveRange(productFollowers);                     
            //刪產品展示留言
            var productComments = Context.ProductComments.Where(tuples => tuples.ProductId == productId);    
                Context.ProductComments.RemoveRange(productComments);
            
            //刪產品歷程+產品歷程圖  產品歷程媒體+檔案   
            var productHistories = Context.ProductHistories.Where(tuples => tuples.ProductId == productId).ToArray();            
            for(int i=0; i < productHistories.Length; i++)
            {
                //找到產品歷程相對應的產品媒體 
                ProductHistoryMedia temp = Context.ProductHistoryMedias.Where(tuples => 
                tuples.ProductHistoryMediaId == productHistories[i].ProductHistoryMediaId).SingleOrDefault();
                //刪產品歷程 + 產品歷程圖
                FileTool.DeleteFile(productHistories[i].ProductHistoryHeadShot);
                Context.ProductHistories.Remove(productHistories[i]);
                //刪除產品媒體
                if (temp != null)
                {
                    FileTool.DeleteFile(temp.ProductHistoryMediaPathOne);
                    FileTool.DeleteFile(temp.ProductHistoryMediaPathTwo);
                    FileTool.DeleteFile(temp.ProductHistoryMediaPathThree);
                    FileTool.DeleteFile(temp.ProductHistoryMediaPathFour);
                    FileTool.DeleteFile(temp.ProductHistoryMediaPathFive);
                    Context.ProductHistoryMedias.Remove(temp);
                }              
            }        
            //刪產品、產品媒體   
            var productMedia = Context.ProductMedias.Where(tuples => tuples.ProductMediaId == product.ProductMediaId).SingleOrDefault();
            FileTool.DeleteFile(product.ProductHeadShotPath);
            Context.Products.Remove(product);
            if (productMedia != null)
            {
                FileTool.DeleteFile(productMedia.ProductMediaPathOne);
                FileTool.DeleteFile(productMedia.ProductMediaPathTwo);
                FileTool.DeleteFile(productMedia.ProductMediaPathThree);
                FileTool.DeleteFile(productMedia.ProductMediaPathFour);
                FileTool.DeleteFile(productMedia.ProductMediaPathFive);
                Context.ProductMedias.Remove(productMedia);
            }           
            Context.SaveChanges();
            return NoContent();           
        }




       
    }
}
