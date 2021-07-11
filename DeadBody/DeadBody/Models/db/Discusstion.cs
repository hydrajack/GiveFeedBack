using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class Discusstion
    {
        public int DiscusstionId { get; set; }
        public int UserId { get; set; }
        public string DiscusstionTitle { get; set; }
        public string DiscusstionContent { get; set; }
        public DateTime DiscusstionDateTime { get; set; }
        public string DiscusstonCategory { get; set; }
    }
}
