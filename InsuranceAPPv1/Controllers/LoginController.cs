using InsuranceAPPv1.Models;
using InsuranceAPPv1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceAPPv1.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private readonly IDapper _dapper;
        public LoginController(IConfiguration config, IDapper dapper)
        {
            _config = config;
               _dapper = dapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Create")]
        public IActionResult Login([FromBody] user login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
               
                
            }

            return response;
        }
        private user AuthenticateUser(user login)
        {

            user user = null;

            var result = Task.FromResult(_dapper.Get<user>($"Select * from [user] where username = '{login.UserName}' and Password =  '{login.Password}'  ", null, commandType: System.Data.CommandType.Text));

            if (result.Result != null)
            {
                user = new user { UserID = result.Result.UserID, Password = result.Result.Password };
            }



            
            return user;
        }


    }
}
