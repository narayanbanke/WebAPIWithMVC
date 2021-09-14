using InsuranceAPPv1.Models;
using InsuranceAPPv1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
//using System.Threading.Tasks;

namespace InsuranceAPPv1.Controllers
{
    public class HomeController : Controller
    {
        
        private IConfiguration _config;
        private readonly IDapper _dapper;
        
        public HomeController(IConfiguration config, IDapper dapper)
        {
            _config = config;
            _dapper = dapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        

        public async Task<ActionResult> LossTypes()
        {
            List<LossTypes> LossTypesinfo = new List<LossTypes>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44326/");
                
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/login/");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var LossTypesResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    LossTypesinfo = JsonConvert.DeserializeObject<List<LossTypes>>(LossTypesResponse);
                    return View(LossTypesinfo);
                }
                else

                
                return View(LossTypesinfo);
            }
        }




        [HttpPost]
        public async Task<ActionResult> Index([Bind] Users user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44326/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string querysting =  user.UserName + "," + user.Password;
               
                HttpResponseMessage response = await client.GetAsync("api/login/" + querysting + " ");

               
                    if (response.StatusCode== System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("LossTypes");
                }
                else
                { 
                    //return Json($" Login failed for {user.UserName}   .");
                    ViewBag.ErrorMessage = "UserName or Password is wrong";
                    return View(user);
                } 
            } 
            
        }
        
        private Users AuthenticateUser(Users login)
        {

            Users user = null;
            var result = Task.FromResult(_dapper.Get<Users>($"Select * from [user] where username = '{login.UserName}' and Password =  '{login.Password}' and Active='1' ", null, commandType: System.Data.CommandType.Text));

            if (result.Result != null)
            {
                user = new Users { UserID = result.Result.UserID, Password = result.Result.Password };
            }




            return user;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
