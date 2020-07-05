using System;

namespace url_shorten_api.DTO
{
    public class URLForReturnDTO
    {
        public URLForReturnDTO()
        {
            this.shortUrl = this.baseUrl + "/" + this.urlCode;
        }

        public string originalURl { get; set; }

        public string urlCode { get; set; }

        public string baseUrl { get; set; }
        public string shortUrl { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}