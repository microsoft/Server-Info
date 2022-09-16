namespace Server_Info.Domain
{
    public class ClientInfo : Info
    {        
        public ClientInfo(HttpContext context, HttpRequest request) : base(context, request)
        {
            Parameters.Add("RemoteIpAddress", context.Connection.RemoteIpAddress?.ToString());
            Parameters.Add("RemotePort", context.Connection.RemotePort.ToString());
            Parameters.Add("LocalIpAddress", context.Connection.LocalIpAddress?.ToString());
            Parameters.Add("LocalPort", context.Connection.LocalPort.ToString());

            //SelecionadosEnv.Add("User-Agent", Request.Headers["User-Agent"]);
            foreach (var header in request.Headers)
            {
                //if (
                //    header.Key.ToString() != "Path"
                //    //&& de.Key.ToString() != "PSModulePath"
                //    //&& de.Key.ToString() != "DOTNET_STARTUP_HOOKS"
                //    && de.Key.ToString() != "ASPNETCORE_AUTO_RELOAD_WS_KEY"
                //    )
                {
                    Parameters.Add(header.Key.ToString(), header.Value.ToString());

                    //EnvVariables.Add(string.Format("{0} = {1};", de.Key, de.Value));
                }

                //EnvVariables.Add(string.Format("{0} = {1};", de.Key, de.Value));
            }
        }  

    }
}
