using CarBuilder.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

List<PaintColor> paints = new List<PaintColor>()
{
    new PaintColor()
    {
        Id = 1,
        Price = 638.05M,
        Color = "Floral White"

    },
    new PaintColor()
    {
        Id = 2,
        Price = 690.24M,
        Color = "Persian Green"
    },
    new PaintColor()
    {
        Id = 3,
        Price = 364.29M,
        Color = "Byzantium Purple"
    },
    new PaintColor()
    {
        Id = 4,
        Price = 415.18M,
        Color = "Space Cadet Blue"
    }
};
List<Interior> interiors = new List<Interior>()
{
    new Interior()
    {
        Id = 1,
        Price = 3904.05M,
        Material = "Chocolate Cosmos Fabric"
    },
    new Interior()
    {
        Id = 2,
        Price = 3952.85M,
        Material = "Bittersweet Fabric"
    },
    new Interior()
    {
        Id = 3,
        Price = 2420.15M,
        Material = "Chamoisee Leather"
    },
    new Interior()
    {
        Id = 4,
        Price = 3744.43M,
        Material = "Gunmetal Leather"
    }
};
List<Technology> technologies = new List<Technology>()
{
    new Technology()
    {
        Id = 1,
        Price = 1008.03M,
        Package = "Basic (basic sound system)"
    },
    new Technology()
    {
        Id = 2,
        Price = 3813.38M,
        Package = "Navigation (includes integrated navigation controls)"
    },
    new Technology()
    {
        Id = 3,
        Price = 4914.09M,
        Package = "Visibility (includes side and rear cameras)"
    },
    new Technology()
    {
        Id = 4,
        Price = 7088.34M,
        Package = "Ultra (includes navigation and visibility)"
    }
};
List<Wheels> wheels = new List<Wheels>()
{
    new Wheels()
    {
        Id = 1,
        Price = 278.13M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        Id = 2
        Price = 365.98M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        Id = 3,
        Price = 397.98M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheels()
    {
        Id = 4,
        Price = 410.25M,
        Style = "18-inch Pair Spoke Black"
    }
};

List<Order> orders = new List<Order>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();


