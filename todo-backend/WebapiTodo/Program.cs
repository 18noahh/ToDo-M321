namespace WebapiTodo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
            var mariaDbConnectionString = Environment.GetEnvironmentVariable("MARIADB_CONNECTION");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[REQ] {context.Request.Method} {context.Request.Path} from {context.Request.Headers["Origin"]}");
                await next();
            });

            app.UseRouting();
            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}
