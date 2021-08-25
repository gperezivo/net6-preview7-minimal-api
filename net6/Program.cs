using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using net6; //Nuestro namespace, contiene la clase WeatherForecast
using net6.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>c.SwaggerDoc("v1", new() { Title = "net6 minimal API", Version = "v1" }));
builder.Services.AddSingleton<IHelloWorldService,MyHelloWorldService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "net6 minimal API v1"));
}

string[] Summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", ()=>{
            
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
});

app.MapGet("/hello-world", async(IHelloWorldService service, ILoggerFactory loggerFactory)=>{
    var logger = loggerFactory.CreateLogger("hello-world");
    logger.LogInformation("GET: hello-world");
    return new{
        message=service.HelloWorld
    };
});
app.MapPost("/hello-world", async(IHelloWorldService service,ILoggerFactory loggerFactory,[FromBody] string name )=>{
    var logger = loggerFactory.CreateLogger("hello-world");
    logger.LogInformation("POST: hello-world");

    return new {message=service.Hello(name)};
});
app.UseHttpsRedirection();
app.Run();







