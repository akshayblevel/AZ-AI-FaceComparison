using AZ_AI_FaceComparison.Interfaces;
using AZ_AI_FaceComparison.Services;
using Azure;
using Azure.AI.Vision.Face;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICompareService, CompareService>();
var endpoint = builder.Configuration["AzureFace:Endpoint"];
var apiKey = builder.Configuration["AzureFace:ApiKey"];

builder.Services.AddSingleton(sp =>
{
    return new FaceClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
