using Microsoft.AspNetCore;

public class Program
{
    //程式進入點
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run(); 
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .ConfigureLogging(logginBuilder =>
        {
            logginBuilder.ClearProviders();
            logginBuilder.AddConsole();
            logginBuilder.AddDebug();
            logginBuilder.AddEventSourceLogger();
        }) //設定Logging提供者
        .UseStartup<Startup>();
}