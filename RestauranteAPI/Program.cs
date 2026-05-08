using RestauranteAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Registrar la conexión y la lógica de datos para que el Controlador pueda usarlos
builder.Services.AddSingleton<ConexionBD>();
builder.Services.AddScoped<UsuarioData>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Esto le enseña al programa cómo generar el menú de Swagger
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi(); // Esto genera solo el JSON
    app.UseSwagger();    // Esto activa Swagger
    app.UseSwaggerUI();  // Esto activa la interfaz visual
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
