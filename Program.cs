using WebApp_Template.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DB service
builder.Services.AddHostedService<DatabaseService>();
builder.Services.AddSingleton<DatabaseService>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
