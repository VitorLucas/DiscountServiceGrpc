using DiscountServiceGrpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<DiscountService>();

var app = builder.Build();

app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Discount gRPC Server is running.");

app.Run();
