using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using url_shorten_api.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using url_shorten_api.DTO;
using url_shorten_api.Models;
using System;

namespace url_shorten_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class URLController : ControllerBase
    {
        private readonly IURLRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public URLController(IURLRepository repo, IConfiguration config, IMapper mapper)
        {
            this._mapper = mapper;
            this._config = config;
            this._repo = repo;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> getURLs()
        {

            var shortURL = await _repo.getURLs();
            var urlToReturn = _mapper.Map<IEnumerable<URLForReturnDTO>>(shortURL);
            return Ok(urlToReturn);
        }
        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{urlCode}", Name = "GetURL")]
        public async Task<IActionResult> getURL(string urlCode)
        {

            var url = await _repo.getURL(urlCode);
            // var urlsToReturn = _mapper.Map<URLForReturnDTO>(url);
            // return Ok(urlsToReturn);
            return Redirect(url.originalURl);
        }

        [AllowAnonymous]
        [HttpGet("{urlCode}/check", Name = "Check")]

        public async Task<IActionResult> checkSlug(string urlCode)
        {

            if (await _repo.SlugExists(urlCode))
            {
                return Ok(false);
            }
            return Ok(true);
        }
        [AllowAnonymous]
        [HttpPost("checkfull", Name = "CheckFullURL")]
        public async Task<IActionResult> checkOriginalURL([FromBody] URLShort req)
        {

            var url = await _repo.checkOriginalURL(req.originalURl);
            if (url?.originalURl.Length > 0)
            {
                return Ok(url);
            }
            return Ok(false);
        }

        // POST api/values
        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateURL([FromBody] URLShort url)
        {

            if (await _repo.URLExists(url))
            {
                var savedURL = await _repo.getURL(url.urlCode);
                var urlReturns = _mapper.Map<URLForReturnDTO>(savedURL);
                urlReturns.shortUrl = urlReturns.baseUrl + "/" + urlReturns.urlCode;
                return Ok(urlReturns);
            }

            if (url.urlCode.Length < 1)
            {
                url.urlCode = String.Format("{0:X}", url.originalURl.GetHashCode());
            }

            url.shortUrl = url.baseUrl + "/" + url.urlCode;


            var userToCreate = _mapper.Map<URLShort>(url);
            var createdURL = await _repo.CreateURL(userToCreate);
            var urlToReturn = _mapper.Map<URLForReturnDTO>(createdURL);
            urlToReturn.shortUrl = urlToReturn.baseUrl + "/" + urlToReturn.urlCode;
            return CreatedAtRoute("GetURL", new { Controller = "URL", urlCode = createdURL.urlCode }, urlToReturn);
        }


    }
}