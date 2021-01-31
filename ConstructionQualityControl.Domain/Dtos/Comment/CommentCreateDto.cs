namespace ConstructionQualityControl.Domain.Dtos
{
    public class CommentCreateDto
    {
        public UserReadDto User { get; set; }
        public OrderReadDto Order { get; set; }
        public string Text { get; set; }
    }
}
