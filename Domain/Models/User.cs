namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
