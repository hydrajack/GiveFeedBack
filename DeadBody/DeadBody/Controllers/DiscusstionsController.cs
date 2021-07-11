using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.DiscusstionsParameter;
using DeadBody.Models.db;
using Microsoft.AspNetCore.Mvc;

 

namespace DeadBody.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class DiscusstionsController : ControllerBase
    {
        private readonly BackEndContext Context;

        public DiscusstionsController(BackEndContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 新增一則討論區發文
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult PostDiscusstion([FromBody]DiscusstionParameter parameter)
        {
            var user = Context.Users.Where(tuples => tuples.UserId == parameter.UserId).SingleOrDefault();
            if(user is null)
            {
                return NotFound("沒有此會員");
            }
            var newDiscusstion = new Discusstion
            {
                UserId = parameter.UserId,
                DiscusstionTitle = parameter.DiscusstionTitle,
                DiscusstionContent = parameter.DiscusstionContent,
                DiscusstonCategory=parameter.DiscusstionCategory
            };
            Context.Discusstions.Add(newDiscusstion);
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetDiscusstion), new { discusstionId = newDiscusstion.DiscusstionId },newDiscusstion);
        }
        /// <summary>
        /// 取得一則討論區發文
        /// </summary>
        /// <param name="discusstionId"></param>
        /// <returns></returns>
        [HttpGet("{discusstionId}")]
        public ActionResult GetDiscusstion(int discusstionId)
        {
            var discusstion = Context.Discusstions.Where(tuples => tuples.DiscusstionId == discusstionId).SingleOrDefault();
            if(discusstion is null)
            {
                return NotFound("無法從發文編號得到相關一則發文");
            }
            return Ok(discusstion);
        }
        /// <summary>
        /// 取得符合搜尋 的所有發文
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("search/{keyword}")]
        public ActionResult GetDiscusstionsByKeyWord(string keyword)
        {
            var discusstions = Context.Discusstions.Where(tuples => tuples.DiscusstionTitle.Contains(keyword));
            if (discusstions.Count() <= 0)
            {
                return NotFound("無法用關鍵字找到相關文章主題");
            }
            return Ok(discusstions);
        }
        /// <summary>
        /// 取得相關分類的發文
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("category/{category}")]
        public ActionResult GetDiscusstionByCategory(string category)
        {
            var discusstions = Context.Discusstions.Where(tuples => tuples.DiscusstonCategory.Contains(category));
            if (discusstions.Count() <= 0)
            {
                return NotFound("無法用關鍵字找到相關文章主題");
            }
            return Ok(discusstions);
        }
        /// <summary>
        /// 取得有關某一會員的所有發文 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public ActionResult GetDiscusstionsByUserId(int userId)
        {
            var discusstions = Context.Discusstions.Where(tuples => tuples.UserId==userId);
            if (discusstions.Count() <= 0)
            {
                return NotFound("無法找到該會員任何發文，或是並無該會員流水號");
            }
            return Ok(discusstions);
        }
        /// <summary>
        /// 取得所有討論區發文
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult GetAllDiscusstions()
        {
            var discusstions = Context.Discusstions;
            if(discusstions.Count()<=0)
            {
                return NotFound("沒有任何一則發文存在");
            }
            return Ok(discusstions);

        }
        /// <summary>
        /// 更新一則討論區發文
        /// </summary>
        /// <param name="discusstionId"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPut("{discusstionId}")]
        public ActionResult PutDiscusstion(int discusstionId,[FromBody] DiscusstionPutParameter parameter)
        {
            var discusstion = Context.Discusstions.Where(tuples => tuples.DiscusstionId == discusstionId).SingleOrDefault();
            if (discusstion is null)
            {
                return NotFound("無法從發文編號得到相關一則發文");
            }
            if(parameter.DiscusstionTitle is not null )
            {
                discusstion.DiscusstionTitle = parameter.DiscusstionTitle;
            }
            if (parameter.DiscusstionContent is not null)
            {
                discusstion.DiscusstionContent = parameter.DiscusstionContent;
            }
            if(parameter.DiscusstionCategory is not null)
            {
                discusstion.DiscusstonCategory = parameter.DiscusstionCategory;
            }
            Context.SaveChanges();
            return CreatedAtAction(nameof(GetDiscusstion), new {discusstionId = discusstion.DiscusstionId},discusstion);

        }
        [HttpDelete("{discusstionId}")]
        public ActionResult DeleteDiscusstion(int discusstionId)
        {
            var discusstion = Context.Discusstions.Where(tuples => tuples.DiscusstionId == discusstionId).SingleOrDefault();
            if(discusstion is null)
            {
                return NotFound("無法從發文編號得到相關一則發文");
            }
            //刪發文下的留言
            var comments = Context.DiscusstionComments.Where(tuples => tuples.DiscusstionId == discusstionId);
            Context.DiscusstionComments.RemoveRange(comments);
            
            //刪追隨發文者
            var followers = Context.DiscusstionFollowers.Where(tuples => tuples.FollowedDiscusstionId == discusstionId);
            Context.DiscusstionFollowers.RemoveRange(followers);         
            Context.Discusstions.Remove(discusstion);
            Context.SaveChanges();
            return NoContent();
        }
       
    }
}
