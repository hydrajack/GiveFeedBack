using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeadBody.Centers;
using DeadBody.Dto;
using DeadBody.Models.db;
using DeadBody.UserFollowerParameter;
using Microsoft.AspNetCore.Mvc;



namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowersController : ControllerBase
    {
        private readonly UserFollowersCenter UserFollowersCenter;
        private readonly IMapper Mapper;

        public UserFollowersController(UserFollowersCenter userFollowersCenter,IMapper mapper)
        {
            UserFollowersCenter = userFollowersCenter;
            Mapper = mapper;
        }
        /// <summary>
        /// 用追隨者流水編號取得資料
        /// </summary>
        /// <param name="userFollowerId"></param>
        /// <returns></returns>
        [HttpGet("{userFollowerId}")]
        public ActionResult GetUserFollower(int userFollowerId)
        {
            var userFollower = UserFollowersCenter.GetUserFollower(userFollowerId);
            if(userFollower is null)
            {
                return NotFound("無資料，無法用追隨者流水編號取得資料");
            }
            
            return Ok(userFollower);
        }
        /// <summary>
        /// 取得該會員的追蹤數與被追蹤數
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("{UserId}/SubscirbeNumbers")]
        public ActionResult GetSubscirbeNumbers(int UserId)
        {
            var followingUsers = UserFollowersCenter.GetFollowing(UserId);
            var followedUsers = UserFollowersCenter.GetFollowed(UserId);

            SubscribeSize subscribeSize = new SubscribeSize
            {
                FollowingCount = followingUsers.Count(),
                FolowedCount = followedUsers.Count()
            };
            return Ok(subscribeSize);

        }
        /// <summary>
        /// 得到會員追蹤者的資料
        /// </summary>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        [HttpGet("{followUserId}/following")]
        public ActionResult GetFollowingUsers(int followUserId)
        {
        
            var followingUsers = UserFollowersCenter.GetFollowingUser(followUserId);
            if (followingUsers.Count()<=0)
            {
                return NotFound("無資料，無法用訂閱方流水編號取得資料");
            }
            var userDto = Mapper.Map<IEnumerable<UserDto>>(followingUsers);
            return Ok(userDto);
              
        }

       /// <summary>
       /// 得到會員被追蹤的資料
       /// </summary>
       /// <param name="followedUserId"></param>
       /// <returns></returns>
        [HttpGet("{followedUserId}/followers")]
        public  ActionResult GetFollowedUsers(int followedUserId)
        {
            var followedUsers = UserFollowersCenter.GetFollowedUser(followedUserId);
            if (followedUsers.Count() <= 0)
            {
                return NotFound("無資料，無法用被訂閱方流水編號取得資料");
            }
            var userDto = Mapper.Map<IEnumerable<UserDto>>(followedUsers);
            return Ok(userDto);
        }


        /// <summary>
        /// 訂閱與退訂
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult Post([FromBody] Subscribe parameter)
        {
            if (UserFollowersCenter.ComfirmUser(parameter.FollowUserId) is null)
            {
                return BadRequest("找不到追隨方流水號會員");
            }
            if (UserFollowersCenter.ComfirmUser(parameter.FollowedUserId) is null)
            {
                return BadRequest("找不到被追隨方流水號會員");
            }
            var userFollower = UserFollowersCenter. GetUserFollower(parameter.FollowUserId, parameter.FollowedUserId);
            if(userFollower is null)
            {
                var newUserFollower = new UserFollower
                {
                    FollowUserId = parameter.FollowUserId,
                    FollowedUserId = parameter.FollowedUserId
                };
                UserFollowersCenter.AddUserFollower(newUserFollower);
                UserFollowersCenter.SaveChanges();
                return CreatedAtAction(nameof(GetUserFollower),new { userFollowerId = newUserFollower.UserFollowerId }, newUserFollower);
            }
            else
            {
                UserFollowersCenter.DeleteUserFollower(userFollower);
                UserFollowersCenter.SaveChanges();
                return NoContent();
            }

        }

    
    }
}
