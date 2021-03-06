﻿using System;

namespace ConstructionQualityControl.Data.Models
{
    public class Report : IEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public byte[] Data { get; set; }
    }
}
