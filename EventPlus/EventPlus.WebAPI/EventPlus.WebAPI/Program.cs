using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using EventPlus.WebAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1 passo

        builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("DefaultConnection")));

        //2. Registrar as repositories (Injecao de dependencia)
        builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository>();
        builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
        builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<IEventoRepository, EventoRepository>();
        // Adiciona o Swagger

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Api de Eventos",
                Description = "Aplicacao para gerenciamento de eventos",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Marcos Vinicius",
                    Url = new Uri("https://www.linkedin.com/in/marcaumdev")
                },
                License = new OpenApiLicense
                {
                    Name = "License de Exemplo",
                    Url = new Uri("https://example.com/license")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT:"
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
            });
        });

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

            app.UseSwagger(options => { });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}