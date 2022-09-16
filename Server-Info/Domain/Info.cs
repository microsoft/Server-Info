namespace Server_Info.Domain
{
    public abstract class Info
    {
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();

        public Info(HttpContext context, HttpRequest request) 
        { 
        }
    }
}
