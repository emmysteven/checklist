using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace Checklist.Infrastructure.Helpers;

public class IpHelper
{
    public static string GetIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                return ip.ToString();
        return string.Empty;
    }
    
    public static DateTime GetDate(string eodDate)
    {
        return DateTime.ParseExact(eodDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}