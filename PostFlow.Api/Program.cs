using PostFlow.Api.Features.MediaAssets.Services;
using Raven.Client.Documents;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDocumentStore>(_ =>
{
    var store = new DocumentStore
    {
        Urls = ["http://127.0.0.1:8080"],
        Database = "PostFlow"
    };

    store.Initialize();

    return store;
});

builder.Services.AddScoped<IMediaAssetService, MediaAssetService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapPost("/media-assets/upload", async (
    IFormFile file,
    IMediaAssetService mediaAssetService) =>
{
    var result = await mediaAssetService.UploadAsync(file);

    return Results.Ok(result);
})
.WithName("UploadMediaAsset")
.DisableAntiforgery();

app.Run();