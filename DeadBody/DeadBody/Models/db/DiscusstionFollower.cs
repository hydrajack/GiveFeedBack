using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class DiscusstionFollower
    {
        public int DiscusstionFollowerId { get; set; }
        public int FollowUserId { get; set; }
        public int FollowedDiscusstionId { get; set; }
    }
}
