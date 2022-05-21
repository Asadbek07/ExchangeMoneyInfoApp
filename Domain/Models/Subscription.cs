namespace Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTimeOffset NotificationTime { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
