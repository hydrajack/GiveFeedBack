using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Models.db;

namespace DeadBody.Centers
{
    public class DataCenter
    {
        private readonly BackEndContext BackEndContext;

        public DataCenter(BackEndContext backEndContext)
        {
            BackEndContext = backEndContext;

        }
        public void SaveChanges()
        {
            BackEndContext.SaveChanges();
        }
        public User GetUserInfoByUserId(int UserId)
        {
            return BackEndContext.Users.Where(info => info.UserId == UserId).SingleOrDefault();
        }      
        public User GetUserInfoByUserAccount(string userAccount)
        {
            return BackEndContext.Users.Where(info => info.UserAccount.Equals(userAccount)).SingleOrDefault();
        }
        public User GetUserInfoByEmail(string userEmail)
        {
            return BackEndContext.Users.Where(info => info.UserEmail.Equals(userEmail)).SingleOrDefault();
        }
        public User GetUserInfoByAccountPassword(string userAccount, string userPassword)
        {
            return BackEndContext.Users.Where(info => info.UserAccount.Equals(userAccount)
            && info.UserPassword.Equals(userPassword)).SingleOrDefault();
        }
        public void AddNewUser(User NewUser)
        {
            BackEndContext.Users.Add(NewUser);
        }
       
        public UserFollower GetUserFollowerByBothUserId(int userId, int followedUserId)
        {
            return BackEndContext.UserFollowers.Where(info => info.FollowUserId == userId
             && info.FollowedUserId == followedUserId).SingleOrDefault();
        }

        public void AddNewUserFollower(UserFollower newUserFollower)
        {
            BackEndContext.UserFollowers.Add(newUserFollower);
        }
         
        public List<User> GetFollowingUser(int followUserId)
        {
            var userFollower = BackEndContext.UserFollowers.Where(tuples => tuples.FollowUserId == followUserId).ToList();
            List<User> followed = new List<User>();
            foreach (var user in userFollower)
            {
                var followedUser = BackEndContext.Users.Where(tuples => tuples.UserId == user.FollowedUserId).SingleOrDefault();
                followed.Add(followedUser);
            }
            return followed;
        }
        public List<User> GetFollowedUser(int followedUserId)
        {
            var userFollower = BackEndContext.UserFollowers.Where(tuples => tuples.FollowedUserId == followedUserId).ToList();
            List<User> followed = new List<User>();
            foreach (var user in userFollower)
            {
                var followedUser = BackEndContext.Users.Where(tuples => tuples.UserId == user.FollowUserId).SingleOrDefault();
                followed.Add(followedUser);
            }
            return followed;
        }
        public IEnumerable<UserFollower> GetFollowing(int followUserId)
        {
            return BackEndContext.UserFollowers.Where(tuples => tuples.FollowUserId == followUserId);
        }
        /// <summary>
        /// 透過被訂閱方流水編號取得所有該會員被訂閱的資料
        /// </summary>
        /// <param name="followedUserId"></param>
        /// <returns></returns>
        public IEnumerable<UserFollower> GetFollowed(int followedUserId)
        {
            return BackEndContext.UserFollowers.Where(tuples => tuples.FollowedUserId == followedUserId);
        }
        /// <summary>
        /// 透過追隨者流水編號取得單一訂閱資料
        /// </summary>
        /// <param name="followersId"></param>
        /// <returns></returns>
        public UserFollower GetUserFollower(int followersId)
        {
            return BackEndContext.UserFollowers.Where(tuples => tuples.UserFollowerId == followersId).SingleOrDefault();
        }
        public UserFollower GetUserFollower(int followUserId, int followedUserId)
        {

            return BackEndContext.UserFollowers.Where(tuples => tuples.FollowUserId == followUserId
            && tuples.FollowedUserId == followedUserId).SingleOrDefault();
        }
        public User ComfirmUser(int userId)
        {
            return BackEndContext.Users.Where(tuples => tuples.UserId == userId).SingleOrDefault();
        }
        public void AddUserFollower(UserFollower userFollower)
        {

            BackEndContext.UserFollowers.Add(userFollower);
            BackEndContext.SaveChanges();
        }
        public void DeleteUserFollower(UserFollower userFollower)
        {
            BackEndContext.UserFollowers.Remove(userFollower);
            BackEndContext.SaveChanges();
        }


    }
}
