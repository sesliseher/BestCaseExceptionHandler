namespace BestCaseExceptionHandler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Endpoint for test errors
            app.MapGet("/test-error/{statusCode}", (int statusCode) =>
            {
                return statusCode switch
                {
                    400 => Results.BadRequest(),
                    401 => Results.Unauthorized(),
                    403 => Results.Forbid(),
                    404 => Results.NotFound(),
                    500 => Results.Problem("Simulated server error"),
                    502 => Results.StatusCode(502),
                    503 => Results.StatusCode(503),
                    504 => Results.StatusCode(504),
                    _ => Results.StatusCode(statusCode)
                    //You can type the test error url next to the url of the main page and then check
                    //test-error/503
                    //test-error/403
                    //test-error/500
                };
            });

            // Middleware for error pages
            app.UseExceptionHandler("/Error/Index"); // General eror page 
            app.UseStatusCodePages(async context =>
            {
                var statusCode = context.HttpContext.Response.StatusCode;
                context.HttpContext.Response.Redirect($"/Error/Index?statusCode={statusCode}");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
