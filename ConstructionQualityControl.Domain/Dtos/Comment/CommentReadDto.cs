using System;

namespace ConstructionQualityControl.Domain.Dtos
{
    public class CommentReadDto
    {
        public UserReadDto User { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
