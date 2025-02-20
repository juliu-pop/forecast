using Microsoft.AspNetCore.OpenApi;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddOpenApi(options =>
                {
                    options.AddDocumentTransformer((document, _, _) =>
                    {
                        document.Info.Title = "Weather API";
                        document.Info.Description = "An API for weather forecast.";
                        document.Info.Version = "v1";

                        return Task.CompletedTask;
                    });
                });

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.MapControllers();

        app.MapOpenApi();

        app.Run();
    }
}