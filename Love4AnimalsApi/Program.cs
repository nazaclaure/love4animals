using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Repositories;
using Love4AnimalsApi.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, ctx, ct) =>
    {
        doc.Info.Title = "Love4Animals API";
        doc.Info.Version = "v1";
        doc.Info.Description = "API REST para la ONG SafeWildlife - gestión de usuarios, campañas, posts y comentarios.";
        return Task.CompletedTask;
    });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddSingleton<ICampaignRepository, CampaignRepository>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();

// Aquí inyectamos el nuevo módulo de Donaciones
builder.Services.AddScoped<IDonationService, DonationService>();
builder.Services.AddSingleton<IDonationRepository, DonationRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Love4Animals API")
               .WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
