using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeadBody.DiscusstionFollowersParameter;
using DeadBody.Dto;
using DeadBody.Models.db;
using DeadBody.ProductFollowersParameter;
using Microsoft.AspNetCore.Mvc;

 

namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscusstionFollowersController : ControllerBase
    {
        private readonly BackEndContext BackEndContext;
        private readonly IMapper Mapper;
       
        public DiscusstionFollowersController(BackEndContext backEndContext,IMapper mapper )
        {
            BackEndContext = backEndContext;
            Mapper = mapper;
        }

        [HttpPost("")]
        public ActionResult PostDiscusstionFollower([FromBody] DiscusstionFollowersPost parameter)
        {
            var user = BackEndContext.Users.Where(tuples => tuples.UserId == parameter.FollowUserId).SingleOrDefault();
            if(user is null)
            {
                return NotFound("查無此會員");
            }
            var discusstion = BackEndContext.Discusstions.Where(tuples => tuples.DiscusstionId == parameter.FollowedDiscusstionId).SingleOrDefault();
            if(discusstion is null)
            {
                return NotFound("查無此評論");
            }
            var FollowerList = BackEndContext.DiscusstionFollowers.Where(tuples => tuples.FollowUserId == parameter.FollowUserId
           && tuples.FollowedDiscusstionId == parameter.FollowedDiscusstionId).SingleOrDefault();
            if (FollowerList is null)
            {
                var newFollowerList = new DiscusstionFollower
                {
                    FollowUserId = parameter.FollowUserId,
                    FollowedDiscusstionId = parameter.FollowedDiscusstionId
                };
                BackEndContext.DiscusstionFollowers.Add(newFollowerList);
                BackEndContext.SaveChanges();
                return CreatedAtAction(nameof(GetFollowerList), new { discusstionFollowerId = newFollowerList.DiscusstionFollowerId }, newFollowerList);
            }
            else
            {
                BackEndContext.DiscusstionFollowers.Remove(FollowerList);
                BackEndContext.SaveChanges();
                return NoContent();
            }
        }
        /// <summary>
        /// 透過文章被追蹤流水號找到文章被追蹤清單
        /// </summary>
        /// <param name="discusstionFollowerId"></param>
        /// <returns></returns>
        [HttpGet("{discusstionFollowerId}")]
        public ActionResult GetFollowerList(int discusstionFollowerId)
        {
            var followerList = BackEndContext.DiscusstionFollowers.Where(tuples => tuples.DiscusstionFollowerId == discusstionFollowerId).SingleOrDefault();
            if (followerList is null)
            {
                return NotFound("無法透過文章被追蹤流水號找到文章被追蹤清單");
            }
     
            return Ok(followerList);
        }
        /// <summary>
        /// 取得文章被那些會員訂閱
        /// </summary>
        /// <param name="FollowedDiscusstionId"></param>
        /// <returns></returns>
        [HttpGet("{followedDiscusstionId}/followers")]
        public ActionResult GetDuscusstionFollowedUser(int followedDiscusstionId)
        {
            var followerList = BackEndContext.DiscusstionFollowers.Where(tuples => tuples.FollowedDiscusstionId == followedDiscusstionId).ToList();
            if (followerList.Count() <= 0)
            {
                return NotFound("無法用被訂閱文章流水號找到任何文章被追蹤清單");
            }
            List<User> users = new List<User>();
            foreach(var discusstion in followerList)
            {
                var user = BackEndContext.Users.Where(tuples => tuples.UserId == discusstion.FollowUserId).SingleOrDefault();
                users.Add(user);
            }
            var userDios= Mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDios);
        }
        /// <summary>
        /// 取得會員訂閱那些資料
        /// </summary>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        [HttpGet("{followUserId}/following")]
        public ActionResult GetUserFollowingDiscusstion(int followUserId)
        {
            var followerList = BackEndContext.DiscusstionFollowers.Where(tuples => tuples.FollowUserId == followUserId).ToList();
            if (followerList.Count() <= 0)
            {
                return NotFound("無法用訂閱方會員流水號取得任何文章被追蹤清單");
            }
            List<Discusstion> discusstions = new List<Discusstion>();
            foreach (var user in followerList )
            {
                var discusstion = BackEndContext.Discusstions.Where(tuples => tuples.DiscusstionId == user.FollowedDiscusstionId).SingleOrDefault();
                discusstions.Add(discusstion);
            }
            return Ok(discusstions);
        }
        [HttpGet("{followedDiscusstionId}/SubscirbeNumbers")]
        public ActionResult GetSubscirbeNumbers(int followedDiscusstionId)
        {
            var followerList = BackEndContext.DiscusstionFollowers.Where(tuples => tuples.FollowedDiscusstionId == followedDiscusstionId);
            SubscribePParameter subscribe = new SubscribePParameter { SubscribeCount = followerList.Count() };
            return Ok(subscribe);
        }


    }
}
