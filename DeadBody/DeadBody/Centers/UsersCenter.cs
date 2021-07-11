using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Models.db;

namespace DeadBody.Centers
{
    public class UsersCenter
    {
        private readonly BackEndContext BackEndContext;

        public UsersCenter(BackEndContext backEndContext)
        {
            BackEndContext = backEndContext;

        }

        public  User GetUserInfoByUserId(int UserId)
        {
            return BackEndContext.Users.Where(info => info.UserId==UserId).SingleOrDefault();
        }

        public User GetUserInfoByUserAccount(string userAccount)
        {
            return BackEndContext.Users.Where(info => info.UserAccount.Equals(userAccount)).SingleOrDefault();
        }

        public User GetUserInfoByEmail(string userEmail)
        {
            return BackEndContext.Users.Where(info => info.UserEmail.Equals(userEmail)).SingleOrDefault();
        }

        public User GetUserInfoByAccountPassword(string userAccount,string userPassword)
        {
            return BackEndContext.Users.Where(info => info.UserAccount.Equals(userAccount) 
            && info.UserPassword.Equals(userPassword)).SingleOrDefault();
        }

        public User GetUserByAccountEmail(string userAccount,string userEmail)
        {
            return BackEndContext.Users.Where(tuples => tuples.UserAccount.Equals(userAccount)
            && tuples.UserEmail.Equals(userEmail)).SingleOrDefault();
        }
        public void AddNewUser(User NewUser)
        {
            BackEndContext.Users.Add(NewUser);
        }
        public void SaveChanges()
        {
            BackEndContext.SaveChanges();
        }

        public UserFollower GetUserFollowerByBothUserId(int userId,int followedUserId)
        {
            return BackEndContext.UserFollowers.Where(info => info.FollowUserId == userId
             && info.FollowedUserId == followedUserId).SingleOrDefault();
        }

        public void AddNewUserFollower(UserFollower newUserFollower)
        {
            BackEndContext.UserFollowers.Add(newUserFollower);
        }
     
    }
}
