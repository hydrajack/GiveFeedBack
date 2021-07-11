using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Models.db;
using DeadBody.ProductCommentsParameter;
using Microsoft.AspNetCore.Mvc;

 

namespace DeadBody.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductCommentsController : ControllerBase
    {
        private readonly BackEndContext Context;
 
        public ProductCommentsController(BackEndContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 新增一則產品留言
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult PostProductComment([FromBody]ProductCommentParameter parameter)
        {
            var user = Context.Users.Where(tuples => tuples.UserId == parameter.UserId).SingleOrDefault();
            if (user is null)
            {
                return NotFound();
            }
            var product = Context.Products.Where(tuples => tuples.ProductId == parameter.ProductId).SingleOrDefault();
            if(product is null)
            {
                return NotFound();
            }
            var Comment = new ProductComment
            {
                ProductId = parameter.ProductId,
                UserId = parameter.UserId,
                CommentContent = parameter.CommentContent
            };
            Context.ProductComments.Add(Comment);
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetCommentsByCommentId), new { commentId = Comment.CommentId }, Comment);

        }
        /// <summary>
        /// 取得一則產品留言，透過產品展示頁留言編號
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet("{commentId}")]
        public ActionResult GetCommentsByCommentId(int commentId)
        {
            var comment = Context.ProductComments.Where(tuples => 
            tuples.CommentId == commentId).SingleOrDefault();          
             
            if (comment is null)
            {
                return NotFound("無法用產品展示頁留言編號取得產品展示頁留言");
            }
           
            return Ok(comment);
        }
        /// <summary>
        /// 取得有關某一產品展示的所有產品留言
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("all/{productId}")]
        public ActionResult GetCommentsByProductId(int productId)
        {
            var comments = Context.ProductComments.Where(tuples => tuples.ProductId == productId).ToList();
            if (comments.Count() <= 0)
            {
                return NotFound("無法用產品建立編號取得產品展示頁留言");
            }
          
            return Ok(comments);
        }
        [HttpGet("user/{userId}")]
        public ActionResult GetCommentsByUserId(int userId)
        {
            var comments = Context.ProductComments.Where(tuples => tuples.UserId == userId);
            if (comments.Count() <= 0)
            {
                return NotFound("無法用會員帳號流水號取得產品展示頁留言");
            }
            
            return Ok(comments);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut("{commentId}")]
        public ActionResult PutComments(int commentId,[FromBody]ProductCommentPutParameter parameter)
        {
            var comment = Context.ProductComments.Where(tuples => 
            tuples.CommentId == commentId).SingleOrDefault();
            if(comment is null)
            {
                return NotFound("無法用產品展示頁留言編號取得產品展示頁留言");
            }
            if(parameter.CommentContent is not null )
            {
                comment.CommentContent = parameter.CommentContent;
            }        
            Context.SaveChanges();
              return CreatedAtAction(nameof(GetCommentsByCommentId), new { commentId = comment.CommentId }, comment);
        }
        /// <summary>
        /// 刪除一則留言
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpDelete("{commentId}")]
        public ActionResult DeleteComments(int commentId)
        {
            var Comment = Context.ProductComments.Where(tuples => 
            tuples.CommentId == commentId).SingleOrDefault();
            if(Comment is null)
            {
                return NotFound("無法用產品展示頁留言編號取得產品展示頁留言");
            }
            Context.ProductComments.Remove(Comment);
            Context.SaveChanges();
            return NoContent();
        }
        /// <summary>
        /// 透過產品建立流水號 刪除所有相關留言
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("all/{productId}")]
        public ActionResult DeleteCommentsByProductId(int productId)
        {
            var Comments = Context.ProductComments.Where(tuples =>
            tuples.ProductId == productId).ToArray();
            if (Comments.Count() <= 0)
            {
                return NotFound("無法用產品建立編號取得產品展示頁留言");
            }  
            for(int i=0; i < Comments.Count(); i++)
            {
                Context.ProductComments.Remove(Comments[i]);
            }
                      
            Context.SaveChanges();
            return NoContent();
        }
    }
}
