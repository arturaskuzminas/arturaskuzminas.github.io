using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MyShop.Areas.Identity.IdentityHostingStartup))]
namespace MyShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}