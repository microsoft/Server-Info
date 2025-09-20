namespace Server_Info.Domain
{
    public class ClientInfo : Info
    {
        public ClientInfo(HttpContext context, HttpRequest request) : base(context, request)
        {
            // Connection Information
            Parameters.Add("RemoteIpAddress", context.Connection.RemoteIpAddress?.ToString());
            Parameters.Add("RemotePort", context.Connection.RemotePort.ToString());
            Parameters.Add("LocalIpAddress", context.Connection.LocalIpAddress?.ToString());
            Parameters.Add("LocalPort", context.Connection.LocalPort.ToString());

           
            // Request Basic Information
            Parameters.Add("Method", request.Method);
            Parameters.Add("Scheme", request.Scheme);
            Parameters.Add("Host", request.Host.ToString());
            Parameters.Add("Path", request.Path);
            Parameters.Add("PathBase", request.PathBase);
            Parameters.Add("QueryString", request.QueryString.ToString());
            Parameters.Add("Protocol", request.Protocol);
            Parameters.Add("ContentType", request.ContentType ?? "N/A");
            Parameters.Add("ContentLength", request.ContentLength?.ToString() ?? "N/A");

            // Query Parameters
            foreach (var query in request.Query)
            {
                Parameters.Add($"<b>Query:</b> {query.Key}", query.Value.ToString());
            }

            // Cookies
            foreach (var cookie in request.Cookies)
            {
                Parameters.Add($"<b>Cookie:</b> {cookie.Key}", cookie.Value);
            }

            // Form Data (if POST request with form data)
            if (request.HasFormContentType && request.Form != null)
            {
                foreach (var form in request.Form)
                {
                    Parameters.Add($"<b>Form:</b> {form.Key}", form.Value.ToString());
                }

                // File uploads
                foreach (var file in request.Form.Files)
                {
                    Parameters.Add($"<b>File: </b> {file.Name}", $"FileName: {file.FileName}, Size: {file.Length} bytes, ContentType: {file.ContentType}");
                }
            }

            // Headers
            foreach (var header in request.Headers)
            {
                Parameters.Add($"<b>Header: </b> {header.Key.ToString()}", header.Value.ToString());
            }

            // User Information (if authenticated)
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                Parameters.Add("IsAuthenticated", "true");
                Parameters.Add("AuthenticationType", context.User.Identity.AuthenticationType ?? "N/A");
                Parameters.Add("UserName", context.User.Identity.Name ?? "N/A");
                
                // Claims
                foreach (var claim in context.User.Claims)
                {
                    Parameters.Add($"<b>Claim:</b> {claim.Type}", claim.Value);
                }
            }
            else
            {
                Parameters.Add("IsAuthenticated", "false");
            }

            // Session Information (with safe handling)
            try
            {
                if (context.Session != null)
                {
                    Parameters.Add("SessionId", context.Session.Id);
                    Parameters.Add("SessionIsAvailable", context.Session.IsAvailable.ToString());
                }
                else
                {
                    Parameters.Add("SessionStatus", "Not configured");
                }
            }
            catch (Exception ex)
            {
                Parameters.Add("SessionError", ex.Message);
            }

            // Security Headers
            Parameters.Add("<b>Security Header:</b> IsHttps", request.IsHttps.ToString());
            Parameters.Add("<b>Security Header:</b> Referer", request.Headers["Referer"].ToString());
            Parameters.Add("<b>Security Header:</b> Origin", request.Headers["Origin"].ToString());

            // Request Body Information
            if (request.ContentLength > 0)
            {
                Parameters.Add("Body: ", request.Body?.ToString());
                Parameters.Add("HasBody", "true");
                Parameters.Add("BodyLength", request.ContentLength.ToString());
            }
            else
            {
                Parameters.Add("HasBody", "false");
            }

            // Request Timing
            Parameters.Add("RequestProcessedAt", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC"));
        }
    }
}
