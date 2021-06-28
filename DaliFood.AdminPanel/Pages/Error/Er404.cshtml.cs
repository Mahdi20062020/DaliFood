using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DaliFood.AdminPanel.Pages.Error
{

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class Er404Model : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<Er404Model> _logger;

        public Er404Model(ILogger<Er404Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }

}
