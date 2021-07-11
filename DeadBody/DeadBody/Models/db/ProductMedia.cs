using System;
using System.Collections.Generic;

#nullable disable

namespace DeadBody.Models.db
{
    public partial class ProductMedia
    {
        public int ProductMediaId { get; set; }
        public string ProductMediaPathOne { get; set; }
        public string ProductMediaVideoPath { get; set; }
        public string ProductMediaPathTwo { get; set; }
        public string ProductMediaPathThree { get; set; }
        public string ProductMediaPathFour { get; set; }
        public string ProductMediaPathFive { get; set; }
    }
}
