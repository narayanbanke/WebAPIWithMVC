using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IDapper _dapper;
        public LoginController(IConfiguration config, IDapper dapper)
        {
            _config = config;
            _dapper = dapper;
        }


        
        [HttpGet]
        public IEnumerable<LossTypes> GetLosstype()
        {
            var result = Task.FromResult(_dapper.GetAll<LossTypes>($"Select * from [LossTypes] ", null, commandType: System.Data.CommandType.Text).ToList());
            return  result.Result  ;
           
        }

        

        
        [HttpGet("{Username},{password}")]
        public Users LoginUser(string Username, string password)
        {
            
            var result = Task.FromResult(_dapper.Get<Users>($"Select * from [Users] where username = '{Username}' and Password =  '{password}' and Active='1' ", null, commandType: System.Data.CommandType.Text)).Result;
            
            return result;
        }
         
        
    }
}
