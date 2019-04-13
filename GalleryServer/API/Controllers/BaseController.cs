using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BaseController : Controller
    {
        protected int? _userId;
        public BaseController()
        {
            try
            {
                _userId = int.Parse(((ClaimsPrincipal)Thread.CurrentPrincipal)?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            }
            catch(Exception)
            {
                _userId = null;
            }
        }
    }
}