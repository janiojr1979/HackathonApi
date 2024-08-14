using Ioc;

namespace HackathonApi
{
    public static class Startup
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddConfigureServices(builder.Configuration);
            builder.Build().ConfigureApp(builder);
            return builder;
        }
        public static WebApplication ConfigureApp(this WebApplication app, WebApplicationBuilder builder)
        {
            app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.UseRouting();
            //app.UseAuthorization();
            app.MapControllers();
            app.Run();
            return app;
        }
    }
}
