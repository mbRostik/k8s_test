using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;

namespace AdminPanel.Configs
{
    public static class KestrelConfiguration
    {
        public static void Configure(WebHostBuilderContext context, KestrelServerOptions options)
        {
            var config = context.Configuration;

            var cert = new X509Certificate2(
                config["Kestrel:Certificates:Default:Path"],
                config["Kestrel:Certificates:Default:Password"]);

            var httpsDefaultPort = int.Parse(config["Kestrel:CustomEndpoints:HttpsDefault:Port"] ?? "7088");
            var httpPort = int.Parse(config["Kestrel:CustomEndpoints:Http:Port"] ?? "5000");
            options.ListenAnyIP(httpsDefaultPort, listen =>
            {
                listen.UseHttps(https =>
                {
                    https.ServerCertificate = cert;
                    https.ClientCertificateMode = ClientCertificateMode.NoCertificate;
                });
            });

            options.ListenAnyIP(httpPort);
        }
    }
}
