using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using url_shorten_api.Models;

namespace url_shorten_api.Data
{
    public class URLRepository : IURLRepository
    {

        private readonly DataContext _context;
        public URLRepository(DataContext context)
        {
            this._context = context;

        }

        public async Task<URLShort> getURL(string urlCode)
        {
            var url = await _context.URLShort.FirstOrDefaultAsync(x => x.urlCode == urlCode);
            return url;
        }

        public async Task<IEnumerable<URLShort>> getURLs()
        {
            var allURLs = await _context.URLShort.ToListAsync();
            return allURLs;
        }

        public async Task<URLShort> CreateURL(URLShort url)
        {
            await _context.URLShort.AddAsync(url); //adding to database
            await _context.SaveChangesAsync();
            return url;

        }

        public async Task<bool> URLExists(URLShort url)
        {
            var shortURL = await _context.URLShort.AnyAsync(x => x.urlCode == url.urlCode);
            //AnyAsync return bool 
            if (shortURL) return true;

            return false;
        }

        public async Task<bool> SlugExists(string urlCode)
        {
            var shortURL = await _context.URLShort.AnyAsync(x => x.urlCode == urlCode);
            //AnyAsync return bool 
            if (shortURL) return true;

            return false;
        }

        public async Task<URLShort> checkOriginalURL(string originalURL)
        {
            var url = await _context.URLShort.FirstOrDefaultAsync(x => x.originalURl == originalURL);
            return url;
        }
    }
}