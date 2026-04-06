/*cs anterior 03/04
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Repositories;
using Love4AnimalsApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddSingleton<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
*/
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Repositories;
using Love4AnimalsApi.Services;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddSingleton<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
