using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.DiscusstipnCommentsParameter;
using DeadBody.Models.db;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscusstionCommentsController : ControllerBase
    {
        private readonly BackEndContext BackEndContext;

        public DiscusstionCommentsController(BackEndContext backEndContext)
        {
            BackEndContext = backEndContext;
        }
        /// <summary>
        /// 新增一則留言
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult PostComment([FromBody]DiscusstionCommentsPost parameter)
        {
            var user = BackEndContext.Users.Where(tuples => tuples.UserId == parameter.UserId).SingleOrDefault();
            if( user is null)
            {
                return NotFound("找不到該會員");
            }
            var discusstion = BackEndContext.Discusstions.Where(tuples => tuples.DiscusstionId == parameter.DiscusstionId).SingleOrDefault();
            if(discusstion is null)
            {
                return NotFound("找不到發文");
            }
            
            var comment = new DiscusstionComment()
            {
                DiscusstionId = parameter.DiscusstionId,
                UserId = parameter.UserId,
                DiscusstionCommentContent = parameter.DiscusstionCommentContent
            };
            BackEndContext.DiscusstionComments.Add(comment);
            BackEndContext.SaveChanges();
            return CreatedAtAction(nameof(GetCommentsByDiscusstionCommentId), 
                new { discusstionId = comment.DiscusstionId },comment);
        }
        /// <summary>
        /// 取得有關某一討論區發文的所有留言
        /// </summary>
        [HttpGet("all/{discusstionId}")]
        public ActionResult GetAllComments(int discusstionId)
        {
            var comments = BackEndContext.DiscusstionComments.Where(tuples => tuples.DiscusstionId == discusstionId);
            if (comments.Count() <= 0)
            {
                return NotFound("無法用發文編號找到任何發文的留言");
            }
            return Ok(comments);

        }
        /// <summary>
        /// 取得有關某一會員的所有文章留言
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public ActionResult GetCommentsByuserId(int userId)
        {
            var comments = BackEndContext.DiscusstionComments.Where(tuples => tuples.UserId == userId );
            if (comments.Count() <= 0)
            {
                return NotFound("無法用發文編號找到任何發文的留言");
            }
            return Ok(comments);
        }
        /// <summary>
        /// 透過討論文章留言編號取得一則留言
        /// </summary>
        /// <param name="discusstionId"></param>
        /// <returns></returns>
        [HttpGet("{discusstionId}")]
        public ActionResult GetCommentsByDiscusstionCommentId(int discusstionId)
        {
            var comment = BackEndContext.DiscusstionComments.Where(tuples => 
            tuples.DiscusstionCommentId == discusstionId).SingleOrDefault();
            if(comment is null)
            {
                return NotFound("無法用討論文章流水編號取得一則留言");
            }
            return Ok(comment);
        }
        /// <summary>
        /// 透過討論文章留言編號修改一則留言
        /// </summary>
        /// <param name="discusstionCommentId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut("{discusstionCommentId}")]
        public ActionResult PutDiscusstionComments(int discusstionCommentId,[FromBody]DiscusstionCommentsPut parameter)
        {
            var comment = BackEndContext.DiscusstionComments.Where(tuples => 
            tuples.DiscusstionCommentId == discusstionCommentId).SingleOrDefault();
            if (comment is null)
            {
                return NotFound("無法用討論文章流水編號取得一則留言");
            }
            if(parameter.DiscusstionCommentContent is not null)
            {
                comment.DiscusstionCommentContent = parameter.DiscusstionCommentContent;
               
            }
            BackEndContext.SaveChanges();
            return CreatedAtAction(nameof(GetCommentsByDiscusstionCommentId),
               new { discusstionId = comment.DiscusstionId}, comment);
        }
        /// <summary>
        /// 透過討論文章留言編號刪除一則留言
        /// </summary>
        [HttpDelete("{discusstionCommentId}")]
        public ActionResult DeleteDiscusstionComments(int discusstionCommentId)
        {
            var comment = BackEndContext.DiscusstionComments.Where(tuples =>
            tuples.DiscusstionCommentId == discusstionCommentId).SingleOrDefault();
            if (comment is null)
            {
                return NotFound("無法用討論文章流水編號取得一則留言");
            }
            BackEndContext.DiscusstionComments.Remove(comment);
            BackEndContext.SaveChanges();
            return NoContent();
        }
        


    }
}
