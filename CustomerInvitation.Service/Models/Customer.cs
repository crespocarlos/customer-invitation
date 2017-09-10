using Newtonsoft.Json;

namespace CustomerInvitation.Models
{
    public class Customer
    {
        [JsonProperty(PropertyName = "user_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}