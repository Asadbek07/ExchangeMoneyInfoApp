namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public int TelegramId { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
