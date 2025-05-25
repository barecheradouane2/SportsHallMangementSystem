using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.interfaces;

namespace Sportshall.Api.Controllers
{
    [Route("errors/{statuscode}")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
       
        

        [HttpGet]
        public IActionResult Error(int statuscode)
        {
            return  new ObjectResult(new  ResponseApi(statuscode));
        }

       
    }
    
}
