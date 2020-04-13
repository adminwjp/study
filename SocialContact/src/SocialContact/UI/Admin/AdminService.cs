using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace SocialContact.UI.Admin
{
    public class AdminService
    {
        private readonly string _route;
        private readonly HttpClient _httpClient;

        public AdminService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _route = configuration["AdminService:ControllerRoute"];
        }
        public async Task<HttpResponseMessage> Login(string returnUrl, string json)
        {
            return await _httpClient.PostAsync($"{_route}/admin/api/v1/admin/login", new  StringContent(json,System.Text.Encoding.UTF8,""));
        }
    }
}
