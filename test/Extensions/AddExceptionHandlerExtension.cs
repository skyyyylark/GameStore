using test.Middlewares;

namespace test.Extensions
{
    public static class AddExceptionHandlerExtension
    {
        public static void AddExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
