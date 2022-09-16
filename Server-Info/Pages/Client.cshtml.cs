using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;

namespace Server_Info.Pages
{
    public class ClientModel : PageModel
    {
        public Dictionary<string, string> Itens = new Dictionary<string, string>();
        public void OnGet()
        {
            Itens = new Factory.InfoFactory().GetInfos("Client", HttpContext, Request);           
        }
    }
}
