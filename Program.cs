var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/getpeople", () =>
{
    List<Person> people = [
        new Person(Guid.NewGuid(),"User1",10),
        new Person(Guid.NewGuid(),"User2",20),
        new Person(Guid.NewGuid(),"User3",30)
    ];
    return people;
})
.WithName("GetPeople")
.WithOpenApi();


app.MapGet("/getperson", (string name) =>
{
    List<Person> people = [
       new Person(Guid.NewGuid(),"User1",10),
        new Person(Guid.NewGuid(),"User2",20),
        new Person(Guid.NewGuid(),"User3",30)
   ];
    return people.SingleOrDefault(x => x.Name == name);
})
.WithName("GetPerson");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


//person object
record Person(Guid Id, string Name, int Age);