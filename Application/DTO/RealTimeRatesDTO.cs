namespace Application.DTO
{

    public class Root
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string source { get; set; }
        public Quotes quotes { get; set; }
    }

    public class Quotes
    {
        public float USDUSD { get; set; }
        public float USDEUR { get; set; }
        public float USDRUB { get; set; }
        public float USDAED { get; set; }
        public float USDKZT { get; set; }
        public float USDLAK { get; set; }
    }

}
