using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NET_Docker_Google__App_Engine.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; private set; } = "Hi there!";
        public string Companies { get; private set; } = "";
        public string Name { get; private set; } = "No One!";

        public void OnGet()
        {
            
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["name"]))
                Name = HttpContext.Request.Query["name"];
        }
    }
}
