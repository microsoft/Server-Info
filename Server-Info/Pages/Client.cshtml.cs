using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;

namespace Server_Info.Pages
{
    public class ClientModel : PageModel
    {
        public Dictionary<string, string> SelecionadosEnv = new Dictionary<string, string>();
        public void OnGet()
        {
            SelecionadosEnv.Add("RemoteIpAddress", HttpContext.Connection.RemoteIpAddress?.ToString());
            SelecionadosEnv.Add("RemotePort", HttpContext.Connection.RemotePort.ToString());
            SelecionadosEnv.Add("LocalIpAddress", HttpContext.Connection.LocalIpAddress?.ToString());
            SelecionadosEnv.Add("LocalPort", HttpContext.Connection.LocalPort.ToString());


            
            //SelecionadosEnv.Add("User-Agent", Request.Headers["User-Agent"]);
            foreach (var header in Request.Headers)
            {
                //if (
                //    header.Key.ToString() != "Path"
                //    //&& de.Key.ToString() != "PSModulePath"
                //    //&& de.Key.ToString() != "DOTNET_STARTUP_HOOKS"
                //    && de.Key.ToString() != "ASPNETCORE_AUTO_RELOAD_WS_KEY"
                //    )
                {
                    SelecionadosEnv.Add(header.Key.ToString(), header.Value.ToString());

                    //EnvVariables.Add(string.Format("{0} = {1};", de.Key, de.Value));
                }

                //EnvVariables.Add(string.Format("{0} = {1};", de.Key, de.Value));
            }
        }
    }
}
