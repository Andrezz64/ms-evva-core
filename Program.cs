using ms_evva_core.Repos.Interfaces;
using ms_evva_core.Repos.Classes;
using ms_evva_core.Services.Interfaces;
using ms_evva_core.Services.Classes;
using ms_evva_core.Utils;
using ms_evva_core.Controllers.WS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationHelper.Initialize(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Repositories
builder.Services.AddScoped<IHostRepository, HostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IDockerConfigRepository, DockerConfigRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IProjectWorkflowRepository, ProjectWorkflowRepository>();
// Services
builder.Services.AddScoped<IHostControllerService, HostControllerService>();
builder.Services.AddScoped<IUserControllerService, UserControllerService>();
builder.Services.AddScoped<ITokenControllerService, TokenControllerService>();
builder.Services.AddScoped<IDockerConfigControllerService, DockerConfigControllerService>();
builder.Services.AddScoped<IProjectControllerService, ProjectControllerService>();
builder.Services.AddScoped<IWorkflowControllerService, WorkflowControllerService>();
builder.Services.AddScoped<IProjectWorkflowControllerService, ProjectWorkflowControllerService>();
builder.Services.AddSingleton<WebSocketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
