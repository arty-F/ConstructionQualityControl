﻿using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class OrderRootReadDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public UserReadDto User { get; set; }
        public string Demands { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
    }
}
