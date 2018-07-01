using Newtonsoft.Json;

namespace SmartPrice
{
    public class Post
    {
        public int Product_Id { get; set; }
        public string Shop { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Post Id: {0}\nTitle: {1}\nBody: {2}",
                Product_Id, Shop, Picture);
        }
    }
}