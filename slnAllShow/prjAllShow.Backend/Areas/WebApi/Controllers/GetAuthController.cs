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
        private readonly ITokenService _tokenService;
        private readonly string apiUrl;
        private readonly string key;
        public GetAuthController(IConfiguration config, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _config = config;
            _userManager = userManager;
            _tokenService = tokenService;
            this.apiUrl = _config.GetSection("WebAPIUrl").Value;
            this.key = _config.GetValue<string>("Jwt:Key");
        }

        [Authorize]
        [HttpGet]
        public async Task<ApiReponse<AuthResult>> GetAuthTokenAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByIdAsync(Convert.ToString(userId));
            string tokenStr;
            string retokenStr;
            //The data that needs to be sent. Any object works.
            var pocoObject = new
            {
                userEmail = userEmail,
                password = user.PasswordHash
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(pocoObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            //The url to post to.
            var url = apiUrl + @"GetAuth/authentication";
            var client = new HttpClient();

            //Pass in the full URL and the json string content
            var response = await client.PostAsync(url, data);

            //It would be better to make sure this request actually made it through
            //tokenStr = await response.Content.ReadAsStringAsync();
            AuthResult res = JsonConvert.DeserializeObject<AuthResult>(await response.Content.ReadAsStringAsync());
            tokenStr = res.Token;
            retokenStr = res.RefreshToken;
            //close out the client
            client.Dispose();
            //using (HttpClient client = new HttpClient())
            //{
            //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", { ServiceCredentialID: ServiceCredentialSecret });

            //    var formContent = new FormUrlEncodedContent(new[]
            //    {
            //        new KeyValuePair<string, string>("grant_type", "client_credentials")
            //    });

            //    using (HttpResponseMessage res = await client.PostAsync(apiUrl+ @"GetAuth/authentication", formContent))
            //    {

            //        using (HttpContent content = res.Content)
            //        {
            //            tokenStr = await content.ReadAsStringAsync();
            //        }
            //    }
            //}
            return new ApiReponse<AuthResult>(
                new AuthResult()
                {
                    Token = res.Token,
                    Success = true,
                    RefreshToken = res.RefreshToken,
                });
        }

        [Authorize]
        [HttpPost("checktokenvalid")]
        public async Task<ApiReponse<AuthResult>> CheckTokenValidAsync([FromBody] TokenRequest request)
        {
            //bool check = _tokenService.IsTokenValid(this.key, view.Token);

            //if (!check)
            //    return new ApiReponse<string>("驗證失敗", "check Token Valid", false);
            //else
            //    return new ApiReponse<string>("驗證成功", "check Token Valid", true);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //The data that needs to be sent. Any object works.
            var pocoObject = new
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken,
                UserId = userId
            };

            //Converting the object to a json string. NOTE: Make sure the object doesn't contain circular references.
            string json = JsonConvert.SerializeObject(pocoObject);

            //Needed to setup the body of the request
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            //The url to post to.
            var url = apiUrl + @"GetAuth/refreshtoken";
            var client = new HttpClient();

            //Pass in the full URL and the json string content
            var response = await client.PostAsync(url, data);

            //It would be better to make sure this request actually made it through
            AuthResult res = JsonConvert.DeserializeObject<AuthResult>(await response.Content.ReadAsStringAsync());

            //close out the client
            client.Dispose();
            if (res.Success)
            {
                return new ApiReponse<AuthResult>(
                    new AuthResult()
                    {
                        Token = res.Token,
                        Success = true,
                        RefreshToken = res.RefreshToken,
                    });
            }
            else
            {
                return new ApiReponse<AuthResult>(res.Errors[0], res, false);
            }
        }
    }
}
