using System.Collections.Generic;
using System.Threading.Tasks;
using url_shorten_api.Models;

namespace url_shorten_api.Data
{
    public interface IURLRepository
    {
        Task<URLShort> getURL(string shortCode); //add sytem.threading.task
        Task<URLShort> checkOriginalURL(string originalURL);
        Task<IEnumerable<URLShort>> getURLs();
        Task<URLShort> CreateURL(URLShort url);
        Task<bool> URLExists(URLShort url);
        Task<bool> SlugExists(string shortCode);

    }
}