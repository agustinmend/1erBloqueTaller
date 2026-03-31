var builder = WebApplication.CreateBuilder(args);

var origenesPermitidos = builder.Configuration
    .GetValue<string>("OrigenesPermitidos")!
    .Split(",");

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(origenesPermitidos)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<Hotel.Repository.Interfaces.IHuespedRepository, Hotel.Repository.Implementaciones.HuespedRepository>();
builder.Services.AddScoped<Hotel.Repository.Interfaces.IReservaRepository, Hotel.Repository.Implementaciones.ReservaRepository>();
builder.Services.AddScoped<Hotel.Repository.Interfaces.IDepartamentoRepository, Hotel.Repository.Implementaciones.DepartamentoRepository>();
builder.Services.AddScoped<Hotel.Repository.Interfaces.IEstadiaRepository, Hotel.Repository.Implementaciones.EstadiaRepository>();
builder.Services.AddScoped<Hotel.Services.Interfaces.IHuespedService, Hotel.Services.Implementaciones.HuespedService>();
builder.Services.AddScoped<Hotel.Services.Interfaces.IReservaService, Hotel.Services.Implementaciones.ReservaService>();
builder.Services.AddScoped<Hotel.Services.Interfaces.IDepartamentoService, Hotel.Services.Implementaciones.DepartamentoService>();
builder.Services.AddScoped<Hotel.Services.Interfaces.IEstadiaService, Hotel.Services.Implementaciones.EstadiaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();