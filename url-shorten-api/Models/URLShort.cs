using System;

namespace url_shorten_api.Models
{
    public class URLShort
    {
        public URLShort()
        {
            this.createdAt = DateTime.Now;
            this.updatedAt = DateTime.Now;


        }
        public int Id { get; set; }

        public string originalURl { get; set; }

        public string urlCode { get; set; }
        public string shortUrl { get; set; }

        public string baseUrl { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

    }
}