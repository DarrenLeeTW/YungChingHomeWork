using YungChingHomeWork.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<HouseListingService>();

var app = builder.Build();

// ...existing code...

app.Run();