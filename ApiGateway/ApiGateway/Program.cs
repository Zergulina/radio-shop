using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("./ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration)
    .AddPolly();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");
    c.SwaggerEndpoint("/users/swagger/v1/swagger.json", "Users Service");
    c.SwaggerEndpoint("/catalog/swagger/v1/swagger.json", "Catalog Service");
    c.SwaggerEndpoint("/cart/swagger/v1/swagger.json", "Cart Service");
    c.SwaggerEndpoint("/images/swagger/v1/swagger.json", "Image Service");
    c.SwaggerEndpoint("/orders/swagger/v1/swagger.json", "Order Service");
    c.SwaggerEndpoint("/rating/swagger/v1/swagger.json", "Rating Service");
});

await app.UseOcelot();

app.Run();