using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Specialized;
using System.Web;

namespace Server_Info.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;       
        public Dictionary<string, string> Itens = new Dictionary<string, string>();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }             

        public void OnGet()
        {
            Itens = new Factory.InfoFactory().GetInfos("Server", HttpContext, Request);
        }



    }
}