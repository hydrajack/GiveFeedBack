using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeadBody.Dto;
using DeadBody.Models.db;
using DeadBody.ProductFollowersParameter;
using Microsoft.AspNetCore.Mvc;

 

namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFollowersController : ControllerBase
    {
        private readonly BackEndContext Context;
        private readonly IMapper Mapper;
        public ProductFollowersController(BackEndContext context,  IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        /// <summary>
        /// 會員訂閱產品 /取消訂閱產品
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost("")]
        public ActionResult PostProductFollower([FromBody]ProductFollowerParameter parameter)
        {
            if (Context.Users.Where(tuples => tuples.UserId==parameter.FollowUserId).SingleOrDefault() is null)
            {
                return NotFound();
            }

            if (Context.Products.Where(tuples => tuples.ProductId
            ==parameter.FollowedProductId).SingleOrDefault() is null)
            {
                return NotFound();
            }

            var FollowerList = Context.ProductFollowers.Where(tuples => tuples.FollowUserId == parameter.FollowUserId 
            && tuples.FollowedProductId == parameter.FollowedProductId).SingleOrDefault();
            if(FollowerList is null)
            {
                var newFollowerList = new ProductFollower
                {
                    FollowUserId = parameter.FollowUserId,
                    FollowedProductId = parameter.FollowedProductId
                };
                Context.ProductFollowers.Add(newFollowerList);
                Context.SaveChanges();
                return CreatedAtAction(nameof(GetProductFollower), new { productFollowerId = newFollowerList.ProductFollowerId}, newFollowerList);
            }
            else
            {
                Context.ProductFollowers.Remove(FollowerList);
                Context.SaveChanges();
                return NoContent();
            }     
        }
        /// <summary>
        /// 取得會員訂閱那些資料產品
        /// </summary>
        /// <param name="followUserId"></param>
        /// <returns></returns>
        [HttpGet("{followUserId}/following")]
        public ActionResult GetUserFollowingProduct(int followUserId)
        {
            var followerList = Context.ProductFollowers.Where(tuples => tuples.FollowUserId == followUserId).ToList();
            if (followerList.Count() <= 0)
            {
                return NotFound("無法用追蹤方流水帳號找到期追蹤的產品");
            }
            List<Product> products = new List<Product>();
            foreach(var user in followerList)
            {
                var product = Context.Products.Where(tuples => tuples.ProductId == user.FollowedProductId).SingleOrDefault();
                products.Add(product);
            }
            return Ok(products);
        }
        /// <summary>
        /// 取得產品被什麼會員訂閱
        /// </summary>
        [HttpGet("{followedProductId}/followers")]
        public ActionResult GetProductFollowedUser(int followedProductId)
        {
            var followerList = Context.ProductFollowers.Where(tuples => tuples.FollowedProductId == followedProductId).ToList();
            if (followerList.Count() <= 0)
            {
                return NotFound("無法用產品展示流水號找到產品追蹤清單");
            }
            List<User> users = new List<User>();
            foreach(var product in followerList)
            {
                var user = Context.Users.Where(tuples => tuples.UserId == product.FollowUserId).SingleOrDefault();
                users.Add(user);
            }
            var userDtos = Mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productFollowerId"></param>
        /// <returns></returns>
        [HttpGet("{productFollowerId}")]
        public ActionResult GetProductFollower(int productFollowerId)
        {
            var followerList = Context.ProductFollowers.Where(tuples => tuples.ProductFollowerId == productFollowerId).SingleOrDefault();
            if(followerList is null)
            {
                return NotFound("無法用文章追隨流水號找到產品追蹤清單");
            }
            return Ok(followerList);
        }
        /// <summary>
        /// 產品被訂閱數
        /// </summary>
        /// <param name="followedProductId"></param>
        /// <returns></returns>
        [HttpGet("{followedProductId}/SubscirbeNumbers")]
        public ActionResult GetSubscirbeNumbers(int followedProductId)
        {
            var followerList = Context.ProductFollowers.Where(tuples => tuples.FollowedProductId == followedProductId);
            SubscribePParameter subscribe = new SubscribePParameter { SubscribeCount = followerList.Count() };
            return Ok(subscribe);

        }


    }
   

}
