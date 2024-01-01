namespace Wolfe.SpaceTraders.Endpoints;

public static class Home
{
    public static WebApplication MapHomeEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => "Hello World!");
        return app;
    }
}