using System.Security.Cryptography;
using AllShowCommon;
using AllShow.Models.Identity;
using AllShowDTO;
using AllShowService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prjAllShow.Backend.Infrastructure;
using System.Security.Claims;
using System.Text;

namespace prjAllShow.Backend.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ITokenService _tokenService;
        private readonly string apiUrl;
        private readonly string aesKey;
        //private readonly string key;
        public GetAuthController(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;

            this.apiUrl = _config.GetSection("WebAPIUrl").Value;
            this.aesKey = _config.GetSection("AES_Key").Value;
        }
        /*
        [Authorize]
        [HttpGet("getauthtoken")]
        public async Task<IActionResult> GetAuthTokenAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));

            string aesEmail = AESUtility.AESEncryptor(userEmail, aesKey);
            string aesPWD = AESUtility.AESEncryptor(user.PasswordHash, aesKey);

            //The data that needs to be sent. Any object works.
            var sendObject = new
            {
                userEmail = aesEmail,
                password = aesPWD
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(sendObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            //The url to post to.
            var url = apiUrl + @"/GetAuth/authentication";
            var client = new HttpClient();

            //Pass in the full URL and the json string content
            var response = await client.PostAsync(url, data);

            //It would be better to make sure this request actually made it through
            //tokenStr = await response.Content.ReadAsStringAsync();
            TokenResult res = JsonConvert.DeserializeObject<TokenResult>(await response.Content.ReadAsStringAsync());
            //tokenStr = res.Token;
            //retokenStr = res.RefreshToken;
            //close out the client
            client.Dispose();

            if (res != null)
            {
                if (res.Success)
                {                    
                    Response.Cookies.Append("AccessToken", res.AccessToken, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });
                    Response.Cookies.Append("RefreshToken", res.RefreshToken, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });

                    return Ok(
                        new TokenResponse()
                        {
                            //AccessToken = res.AccessToken,
                            TokenType = "Bearer",
                            //RefreshToken = res.RefreshToken
                        });
                }
                else
                {
                    return BadRequest(
                        new FailedResponse()
                        {
                            Errors = res.Errors
                        });
                }
            }
            else
            {
                return BadRequest(
                        new FailedResponse()
                        {
                            Errors = new [] { "Get token failed" }
                        });
            }
        }
        */
        [Authorize]
        [HttpGet("checktokenvalid")]
        public async Task<IActionResult> CheckTokenValidAsync()
        {
            //bool check = _tokenService.IsTokenValid(this.key, view.Token);

            //if (!check)
            //    return new ApiReponse<string>("驗證失敗", "check Token Valid", false);
            //else
            //    return new ApiReponse<string>("驗證成功", "check Token Valid", true);
            if (!Request.Cookies.TryGetValue("AccessToken", out string Token))
            {
                Token = "null";
            }
            if (!Request.Cookies.TryGetValue("RefreshToken", out string RefreshToken))
            {
                RefreshToken = "null";
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));

            string aesEmail = AESUtility.AESEncryptor(userEmail, aesKey);
            string aesPWD = AESUtility.AESEncryptor(user.PasswordHash, aesKey);
            //The data that needs to be sent. Any object works.
            var sendObject = new
            {
                Token = Token,
                RefreshToken = RefreshToken,
                UserEmail = aesEmail,
                Password = aesPWD
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(sendObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            //The url to post to.
            var url = apiUrl + @"/GetAuth/refreshtoken";
            TokenResult result = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                //Pass in the full URL and the json string content
                var response = await client.PostAsync(url, data);

                //It would be better to make sure this request actually made it through
                result = JsonConvert.DeserializeObject<TokenResult>(await response.Content.ReadAsStringAsync());

                //close out the client
            }
            if (result != null)
            {
                if (!result.Success)
                {
                    return Unauthorized(new FailedResponse()
                    {
                        Errors = result.Errors
                    });
                }
                else
                {
                    DateTime loginDt = DateTime.UtcNow;

                    Response.Cookies.Delete("AccessToken");
                    Response.Cookies.Delete("RefreshToken");
                    Response.Cookies.Append("AccessToken", result.AccessToken, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });
                    Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });
                    DateTime dt = loginDt.AddSeconds(result.ExpiresIn);
                    string dts = dt.Ticks.ToString();
                    Response.Cookies.Append("Expires", dts, new CookieOptions()
                    {
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true
                    });
                    return Ok();
                }
            }
            else
                return BadRequest();
        }

        [Authorize]
        [HttpGet("checktokenexpires")]
        public async Task<IActionResult> CheckTokenExpires()
        {
            if (!Request.Cookies.TryGetValue("Expires", out string Expires))
            {
                Expires = "null";
            }

            if (Expires != "null")
            {
                DateTime dateTime = DateTime.UtcNow;
                long exl = Convert.ToInt64(Expires);
                DateTime expiryDate = new DateTime(exl);
                if (expiryDate < dateTime)
                {
                    var res = await CheckTokenValidAsync();
                    return res;
                }
                else
                {
                    return Ok();
                }
            }
            else
                return BadRequest();
        }
    }
}
