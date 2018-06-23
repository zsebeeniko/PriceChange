using Newtonsoft.Json;

namespace SmartPrice
{
    public class Post
    {
        public int product_Id { get; set; }
        public string shop { get; set; }
        public string description { get; set; }
        public byte[] picture { get; set; }

        [JsonProperty("body")]
        public byte[] Content { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Post Id: {0}\nTitle: {1}\nBody: {2}",
                product_Id, shop, Content);
        }
    }
}