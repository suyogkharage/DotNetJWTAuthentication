using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace WebAppWithJWT.Pages
{
    public class SecurePageModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public SecurePageModel(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public string ApiData { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (string.IsNullOrEmpty(token))
            {
                ApiData = "Not logged in.";
                return;
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(_config["ApiBaseUrl"] + "/api/Values/all");

            if (response.IsSuccessStatusCode)
            {
                ApiData = await response.Content.ReadAsStringAsync();
            }
            else
            {
                ApiData = $"API call failed: {response.StatusCode}";
            }
        }
    }
}
