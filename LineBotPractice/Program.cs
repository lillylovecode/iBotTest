using Microsoft.AspNetCore;

public class Program
{
    //�{���i�J�I
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run(); 
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
}