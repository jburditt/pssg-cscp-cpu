using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gov.Cscp.Victims.Public.Services;
using Gov.Cscp.Victims.Public.Models;
using Gov.Cscp.Victims.Public.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Gov.Cscp.Victims.Public.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        // ************* FROM PILL PRESS *****************

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _env;
        private readonly Authentication.SiteMinderAuthOptions _options = new Authentication.SiteMinderAuthOptions();
        private IDynamicsResultService _dynamicsResultService;

        public LoginController(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Login(string path)
        {
            string basePath = string.IsNullOrEmpty(_configuration["BASE_PATH"]) ? "/" : _configuration["BASE_PATH"];
            // we want to redirect to the dashboard if the user is a returning user.

            // get UserSettings from the session
            string temp = _httpContextAccessor.HttpContext.Session.GetString("UserSettings");
            string dashboard = "authenticated/dashboard";
            return Redirect(basePath + "/" + dashboard);
        }

        // /// <summary>
        // /// Injects an authentication token cookie into the response for use with the 
        // /// SiteMinder authentication middleware
        // /// </summary>
        [HttpGet]
        [Route("token/{userid}")]
        [AllowAnonymous]
        public virtual IActionResult GetDevAuthenticationCookie(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest("Missing required userid query parameter.");

            if (userId.ToLower() == "default")
                userId = _options.DevDefaultUserId;

            // clear session
            HttpContext.Session.Clear();

            // expire "dev" user cookie
            string temp = HttpContext.Request.Cookies[_options.DevBCSCAuthenticationTokenKey];
            if (temp == null)
            {
                temp = "";
            }
            Response.Cookies.Append(
                _options.DevBCSCAuthenticationTokenKey,
                temp,
                new CookieOptions
                {
                    Path = "/",
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(-1)
                }
            );
            // create new "dev" user cookie
            Response.Cookies.Append(
                _options.DevAuthenticationTokenKey,
                userId,
                new CookieOptions
                {
                    Path = "/",
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7)
                }
            );

            string basePath = string.IsNullOrEmpty(_configuration["BASE_PATH"]) ? "" : _configuration["BASE_PATH"];
            // always send the user to the business profile.
            string businessprofile = "business-profile";
            basePath += "/" + businessprofile;

            return Redirect(basePath);
        }
    }
}