using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class DiscusstionComment
    {
        public int DiscusstionCommentId { get; set; }
        public int DiscusstionId { get; set; }
        public int UserId { get; set; }
        public string DiscusstionCommentContent { get; set; }
        public DateTime DiscusstionCommentDateTime { get; set; }
    }
}
