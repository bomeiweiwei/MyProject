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
        //private readonly string key;
        public GetAuthController(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;

            this.apiUrl = _config.GetSection("WebAPIUrl").Value;
            //this.key = _config.GetValue<string>("Jwt:Key");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAuthTokenAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));
            //string tokenStr;
            //string retokenStr;
            //The data that needs to be sent. Any object works.
            var sendObject = new
            {
                userEmail = userEmail,
                password = user.PasswordHash
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
                    return Ok(
                        new TokenResponse()
                        {
                            AccessToken = res.AccessToken,
                            TokenType = "Bearer",
                            RefreshToken = res.RefreshToken
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

        [Authorize]
        [HttpPost("checktokenvalid")]
        public async Task<IActionResult> CheckTokenValidAsync([FromBody] TokenRequest request)
        {
            //bool check = _tokenService.IsTokenValid(this.key, view.Token);

            //if (!check)
            //    return new ApiReponse<string>("驗證失敗", "check Token Valid", false);
            //else
            //    return new ApiReponse<string>("驗證成功", "check Token Valid", true);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //The data that needs to be sent. Any object works.
            var sendObject = new
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken,
                UserId = userId
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(sendObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            //The url to post to.
            var url = apiUrl + @"/GetAuth/refreshtoken";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Token);
            //Pass in the full URL and the json string content
            var response = await client.PostAsync(url, data);

            //It would be better to make sure this request actually made it through
            TokenResult result = JsonConvert.DeserializeObject<TokenResult>(await response.Content.ReadAsStringAsync());

            //close out the client
            client.Dispose();
            if (!result.Success)
            {
                return Unauthorized(new FailedResponse()
                {
                    Errors = result.Errors
                });
            }

            return Ok(new TokenResponse
            {
                AccessToken = result.AccessToken,
                TokenType = result.TokenType,
                ExpiresIn = result.ExpiresIn,
                RefreshToken = result.RefreshToken
            });
        }
    }
}
