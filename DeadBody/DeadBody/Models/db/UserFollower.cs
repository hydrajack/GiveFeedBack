using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class UserFollower
    {
        public int UserFollowerId { get; set; }
        public int FollowUserId { get; set; }
        public int FollowedUserId { get; set; }
    }
}
