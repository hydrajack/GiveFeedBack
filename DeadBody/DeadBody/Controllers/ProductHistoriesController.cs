using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Models.db;
using DeadBody.ProductHistoriesParameter;
using DeadBody.Tools;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductHistoriesController : ControllerBase
    {
        private readonly BackEndContext Context;
        private readonly FileTool FileTool;

        public ProductHistoriesController(BackEndContext context,FileTool fileTool)
        {
            Context = context;
            FileTool = fileTool;
        }

        /// <summary>
        /// 新增 一個產品歷程
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult PostProductHistory([FromForm] ProductHistoryPost parameter)
        {

            var product = Context.Products.Where(tuples => tuples.ProductId == parameter.ProductId).SingleOrDefault();
            if(product is null)
            {
                return NotFound("產品流水號輸入錯誤，找不到相關產品");
            }
            var historyMedia = Context.ProductHistoryMedias.Where(tuples => tuples.ProductHistoryMediaId == parameter.ProductHistoryMediaId);
            if(historyMedia is null)
            {
                return NotFound("產品歷程媒體號輸入錯誤，找不到相關產品");
            }
            var history = new ProductHistory
            {
                ProductId=parameter.ProductId,
                ProductHistoryTitle=parameter.ProductHistoryTitle,
                ProductHistoryMediaId=parameter.ProductHistoryMediaId,
                ProductHistoryContent=parameter.ProductHistoryContent,
                ProductHistoryVersion=parameter.ProductHistoryVersion,
            };
            Context.ProductHistories.Add(history);
           
            if(parameter.HeadShotFile is not null)
            {        
                history.ProductHistoryHeadShot = FileTool.SaveImageFile(parameter.HeadShotFile, "ProductHistoryHeadShot", history.ProductHistoryId, 0);              
            }
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetByProductHistoryId),new { productHistoryId =history.ProductHistoryId},history);

        }
        /// <summary>
        /// 用產品歷程編號取得一則產品歷程
        /// </summary>
        /// <param name="productHistoryId"></param>
        /// <returns></returns>
        [HttpGet("{productHistoryId}")]
        public ActionResult GetByProductHistoryId(int productHistoryId)
        {
            var History = Context.ProductHistories.Where(tuple => tuple.ProductHistoryId == productHistoryId).SingleOrDefault();
            if(History is null)
            {
                return NotFound("無法用產品歷程編號取得產品歷程");
            }
            return Ok(History);

        }
        /// <summary>
        /// 透過產品編號取得相關產品歷程
        /// </summary>
        [HttpGet("all/{productId}")]
        public ActionResult GetByProductId(int productId)
        {
            var Histories = Context.ProductHistories.Where(tuples => tuples.ProductId == productId);
            if (Histories.Count()<= 0)
            {
                return NotFound("無法用產品建立編號取得任何產品歷程");
            }
            return Ok(Histories);
        }
        /// <summary>
        /// 更新一則產品歷程的所有欄位
        /// </summary>
        /// <param name="productHistoryId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut("{productHistoryId}")]
        public ActionResult PutByProductHistoryId(int productHistoryId,[FromForm]ProductHistoryPut parameter)
        {
            var history = Context.ProductHistories.Where(tuples => tuples.ProductHistoryId == productHistoryId).SingleOrDefault();
            if(history is null)
            {
                return NotFound("查無會員");
            }
            if(parameter.ProductHistoryTitle is not null)
            {
                history.ProductHistoryTitle = parameter.ProductHistoryTitle;
            }
            if (parameter.HeadShotFile is not null)
            {     
                history.ProductHistoryHeadShot = FileTool.SaveImageFile(parameter.HeadShotFile, "ProductHistoryHeadShot", history.ProductHistoryId, 0);
            }
            if (parameter.ProductHistoryContent is not null)
            {
                history.ProductHistoryContent = parameter.ProductHistoryContent;
            }
            if(parameter.ProductHistoryVersion is not null)
            {
                history.ProductHistoryVersion = parameter.ProductHistoryVersion;
            }
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetByProductHistoryId), new { productHistoryId = history.ProductHistoryId }, history);

        }
        /// <summary>
        /// 刪除一則產品歷程
        /// </summary>
        /// <param name="productHistoryId"></param>
        /// <returns></returns>
        [HttpDelete("{productHistoryId}")]
        public ActionResult DeleteByProductHistoryId(int productHistoryId)
        {
            var productHistory = Context.ProductHistories.Where(tuple => tuple.ProductHistoryId == productHistoryId).SingleOrDefault();
            if (productHistory is null)
            {
                return NotFound("無法用產品歷程編號取得產品歷程");
            }
            FileTool.DeleteFile(productHistory.ProductHistoryHeadShot);
            //刪除產品歷程媒體  沒有寫刪檔案
            var productHistoryMedia = Context.ProductHistoryMedias.Where(tuples => tuples.ProductHistoryMediaId == productHistory.ProductHistoryMediaId).SingleOrDefault();
            Context.ProductHistories.Remove(productHistory);
            if (productHistoryMedia != null)
            {
                FileTool.DeleteFile(productHistoryMedia.ProductHistoryMediaPathOne);
                FileTool.DeleteFile(productHistoryMedia.ProductHistoryMediaPathTwo);
                FileTool.DeleteFile(productHistoryMedia.ProductHistoryMediaPathThree);
                FileTool.DeleteFile(productHistoryMedia.ProductHistoryMediaPathFour);
                FileTool.DeleteFile(productHistoryMedia.ProductHistoryMediaPathFive);
            }
            Context.ProductHistoryMedias.Remove(productHistoryMedia);
            Context.SaveChanges();
            return NoContent();
        }


        
       


    }
}
