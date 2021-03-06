using YolvaTestWork.GeoServices;
using YolvaTestWork.GeoServices.OSM.TypePolygon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Scan(scan => scan
    .FromAssemblyOf<IGeoService>().AddClasses().AsImplementedInterfaces()
    .FromAssemblyOf<IPolygon>().AddClasses().AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services.AddControllers().AddNewtonsoftJson();
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

app.UseAuthorization();

app.MapControllers();

app.Run();