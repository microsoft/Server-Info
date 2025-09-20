using System.Collections;

namespace Server_Info.Domain
{
    public class ServerInfo : Info    
    {        
        public ServerInfo(HttpContext context, HttpRequest request) : base(context, request)
        {
            Parameters.Add("Machine Name", Environment.MachineName);
            Parameters.Add("User Domain Name", Environment.UserDomainName);
            Parameters.Add("User Name", Environment.UserName);
            Parameters.Add("User Interactive", Environment.UserInteractive.ToString());
            Parameters.Add("User OS Version", Environment.OSVersion.ToString());
            Parameters.Add("Tick Count", (Environment.TickCount / (1000 * 60 * 60)) + " Hours");

            List<string> logical = Environment.GetLogicalDrives().ToList<string>();
            string result = "";
            foreach (string item in logical)
            {
                result = result + string.Format("{0}, ", item);
            }
            Parameters.Add("Logical Drivers", result);

            IDictionary environmentVariables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry de in environmentVariables)
            {
                if (
                    de.Key.ToString() != "Path"
                    //&& de.Key.ToString() != "PSModulePath"
                    //&& de.Key.ToString() != "DOTNET_STARTUP_HOOKS"
                    && de.Key.ToString() != "ASPNETCORE_AUTO_RELOAD_WS_KEY"
                    )
                {
                    Parameters.Add(de.Key.ToString(), de.Value.ToString());

                    //EnvVariables.Add(string.Format("{0} = {1};", de.Key, de.Value));
                }
                //EnvVariables.Add(string.Format("{0} = {1};", de.Key, de.Value));
            }
        }
    }
}
