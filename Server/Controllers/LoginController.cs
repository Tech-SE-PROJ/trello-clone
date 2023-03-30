using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trello_clone.Server.Interfaces;
using trello_clone.Server.Services;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        
        public LoginController()
        {
            _loginService = new LoginService();
        }

        [HttpGet("Register/{userName}/{userPass}/{userEmail}")]
        public void Register(string userName, string userPass, string userEmail)
        {
            _loginService.UserRegistration(userName, userPass, userEmail);
        }

        [HttpGet("Login/{userName}/{userPass}")]
        public void Login(string userName, string userPass)
        {
            _loginService.GetUserAccount(userName, userPass);
        }
    }
}