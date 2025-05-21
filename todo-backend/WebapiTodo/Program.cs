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

            var app = builder.Build();
            app.MapControllers();
            app.Run();
        }
    }
}
