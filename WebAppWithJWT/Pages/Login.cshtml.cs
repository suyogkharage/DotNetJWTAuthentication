using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using WebAppWithJWT.Models;

namespace WebAppWithJWT.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        public LoginModel(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        [BindProperty]
        public LoginRequest LoginData { get; set; } = new();

        public string ErrorMessage { get; set; } = string.Empty;
        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(LoginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Call API Login endpoint
            var response = await client.PostAsync(_config["ApiBaseUrl"] + "/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var respContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(respContent);

                // Store JWT token in Session
                HttpContext.Session.SetString("JWToken", loginResponse!.Token);

                return RedirectToPage("/SecurePage"); // redirect after login
            }
            else
            {
                ErrorMessage = "Invalid username or password";
                return Page();
            }
        }
    }
}
