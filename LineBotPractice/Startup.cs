using LineBotPractice.Models;

public class Startup
{
    //注入Configuration組態相依性
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    
    //DI Container相依性注入容器：註冊服務介面與實作相依性
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<LineBotConfig, LineBotConfig>((s) => new LineBotConfig
        {
            ChannelSecret = Configuration["LineBot:channelSecret"],
            AccessToken = Configuration["LineBot:accessToken"]
        });

        services.AddHttpContextAccessor();
        services.AddRazorPages();
    }

    //使用middleware元件：設定HTTP管線使用哪些中介元件的地方
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); //開發者例外頁
        }
        //else
        //{
        //    app.UseExceptionHandler("Home/Error"); //一般例外頁
        //}

        app.UseRouting();//使用路由

        //端點路由
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "api",
                pattern: "api/{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }

    
}