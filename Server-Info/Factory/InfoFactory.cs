using Microsoft.AspNetCore.SignalR;

namespace Server_Info.Factory
{
    public class InfoFactory
    {
        public Dictionary<string, string> GetInfos(string tipo, HttpContext context, HttpRequest request)
        { 
            var infos = new Dictionary<string, string>();
            if (tipo.Equals("Server"))
            {
                Domain.Info _info;
                _info = new Domain.ServerInfo(context, request);
                infos = _info.Parameters;
            }

            if (tipo.Equals("Client"))
            {
                Domain.Info _info;
                _info = new Domain.ClientInfo(context, request);
                infos = _info.Parameters;
            }

            return infos;
        }
    }
}
