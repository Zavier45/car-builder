using CarBuilder.Models;
using CarBuilder.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

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
        Material = "Liver Leather"
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
List<Wheel> wheels = new List<Wheel>()
{
    new Wheel()
    {
        Id = 1,
        Price = 278.13M,
        Style = "17-inch Pair Radial"
    },
    new Wheel()
    {
        Id = 2,
        Price = 365.98M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheel()
    {
        Id = 3,
        Price = 397.98M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheel()
    {
        Id = 4,
        Price = 410.25M,
        Style = "18-inch Pair Spoke Black"
    }
};

List<Order> orders = new List<Order>()
{
    new Order()
    {
        Id = 1,
        TimeStamp = DateTime.Now,
        WheelId = 1,
        TechnologyId = 4,
        PaintId = 1,
        InteriorId = 4
    }
};
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
    {
        options.AllowAnyOrigin();
        options.AllowAnyMethod();
        options.AllowAnyHeader();
    });
}

app.UseHttpsRedirection();

app.MapGet("/wheels", () =>
{
    return wheels.Select(w => new WheelDTO()
    {
        Id = w.Id,
        Price = w.Price,
        Style = w.Style
    });
});

app.MapGet("/paintcolors", () =>
{
    return paints.Select(p => new PaintColorDTO
    {
        Id = p.Id,
        Price = p.Price,
        Color = p.Color
    });
});

app.MapGet("/interiors", () =>
{
    return interiors.Select(i => new InteriorDTO
    {
        Id = i.Id,
        Price = i.Price,
        Material = i.Material
    });
});

app.MapGet("/technologies", () =>
{
    return technologies.Select(t => new TechnologyDTO
    {
        Id = t.Id,
        Price = t.Price,
        Package = t.Package
    });
});

app.MapGet("/orders", () =>
{
    List<Order> ordersTBC = orders.Where(oc => oc.Complete == false).ToList();
    return ordersTBC.Select(o =>
    {
        Wheel wheel = wheels.FirstOrDefault(w => w.Id == o.WheelId);
        Technology tech = technologies.FirstOrDefault(t => t.Id == o.TechnologyId);
        PaintColor paint = paints.FirstOrDefault(p => p.Id == o.PaintId);
        Interior interior = interiors.FirstOrDefault(i => i.Id == o.InteriorId);

        var orderDTO = new OrderDTO
        {
            Id = o.Id,
            TimeStamp = o.TimeStamp,
            WheelId = o.WheelId,
            Wheel = wheel == null ? null : new WheelDTO
            {
                Id = wheel.Id,
                Price = wheel.Price,
                Style = wheel.Style
            },
            TechnologyId = o.TechnologyId,
            Technology = tech == null ? null : new TechnologyDTO
            {
                Id = tech.Id,
                Price = tech.Price,
                Package = tech.Package
            },
            PaintId = o.PaintId,
            Paint = paint == null ? null : new PaintColorDTO
            {
                Id = paint.Id,
                Price = paint.Price,
                Color = paint.Color
            },
            InteriorId = o.InteriorId,
            Interior = interior == null ? null : new InteriorDTO
            {
                Id = interior.Id,
                Price = interior.Price,
                Material = interior.Material
            },
        };
        return orderDTO;
    });
});


app.MapPost("/orders", (NewOrderDTO order) =>
{
    Order newOrder = new Order()
    {
        WheelId = order.WheelId,
        TechnologyId = order.TechnologyId,
        PaintId = order.PaintId,
        InteriorId = order.InteriorId

    };
    int maxOrderId = orders.Any() ? orders.Max(o => o.Id) : 0;
    newOrder.Id = maxOrderId + 1;
    orders.Add(newOrder);

    Wheel wheel = wheels.FirstOrDefault(w => w.Id == order.WheelId);
    Technology tech = technologies.FirstOrDefault(t => t.Id == order.TechnologyId);
    PaintColor paint = paints.FirstOrDefault(p => p.Id == order.PaintId);
    Interior interior = interiors.FirstOrDefault(i => i.Id == order.InteriorId);



    return Results.Created($"/orders/{newOrder.Id}", new OrderDTO
    {

        Id = newOrder.Id,
        TimeStamp = DateTime.Today,
        WheelId = order.WheelId,
        Wheel = wheel == null ? null : new WheelDTO
        {
            Id = wheel.Id,
            Price = wheel.Price,
            Style = wheel.Style
        },
        TechnologyId = order.TechnologyId,
        Technology = tech == null ? null : new TechnologyDTO
        {
            Id = tech.Id,
            Price = tech.Price,
            Package = tech.Package
        },
        PaintId = order.PaintId,
        Paint = paint == null ? null : new PaintColorDTO
        {
            Id = paint.Id,
            Price = paint.Price,
            Color = paint.Color
        },
        InteriorId = order.InteriorId,
        Interior = interior == null ? null : new InteriorDTO
        {
            Id = interior.Id,
            Price = interior.Price,
            Material = interior.Material
        },
        Complete = false
    });
});

app.MapPut("/orders/{id}/fulfill", (int id, Order order) =>
{
    Order completeOrder = orders.FirstOrDefault(co => co.Id == id);
    if (completeOrder == null)
    {
        return Results.NotFound();
    }
    if (id != completeOrder.Id)
    {
        return Results.BadRequest();
    }
    completeOrder.Complete = true;
    return Results.Ok();
});
app.Run();


