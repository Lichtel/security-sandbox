using System.Text.Json.Serialization;

namespace TokenConsumer.Dto
{
    public class KeyResponse
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}