using Microsoft.AspNetCore.Http;

namespace API.Core.Utils
{
    public static class JobUtils
    {
        public static bool FileIsLocked(string filename, FileAccess file_access)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open, file_access);
                fs.Close();
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string? GetClientIp(HttpContext? httpContext)
        {
            if (httpContext == null)
                return "";

            var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',').First().Trim(); // Primeiro IP é o do cliente real
            }

            return "" + httpContext.Connection.RemoteIpAddress?.ToString();
        }

    }
}
