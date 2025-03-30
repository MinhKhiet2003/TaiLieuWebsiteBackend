using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaiLieuWebsiteBackend.Data;
using TaiLieuWebsiteBackend.Repositories;
using TaiLieuWebsiteBackend.Services;
using TaiLieuWebsiteBackend.Services.IServices;
using TaiLieuWebsiteBackend.Component;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TaiLieuWebsiteBackend.Component.Middleware;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetIsOriginAllowed(x => true);
    });
});

// Configure DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Cấu hình Authentication và JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });
builder.Services.AddSwaggerGen(options =>
{
    var JwtSecurityScheme = new OpenApiSecurityScheme
    {

        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Hãy nhập AccessToken để sử dụng API ",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    options.AddSecurityDefinition("Bearer", JwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {JwtSecurityScheme,Array.Empty<string>()}
    });
});

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanModifyVideo", policy =>
        policy.RequireAssertion(context =>
        {
            // Lấy role và user id từ claims
            var isAdmin = context.User.IsInRole("admin");
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Lấy video từ route data (cần truyền video vào context)
            if (context.Resource is HttpContext httpContext)
            {
                var videoId = httpContext.Request.RouteValues["id"]?.ToString();
                if (!string.IsNullOrEmpty(videoId) && int.TryParse(videoId, out var id))
                {
                    var videoService = httpContext.RequestServices.GetRequiredService<IVideoService>();
                    var video = videoService.GetVideoById(id);
                    if (video == null) return false;

                    return isAdmin || userId == video.uploaded_by.ToString();
                }
            }
            return false;
        }));
});
// Register custom services
builder.Services.AddSingleton<JwtTokenUtil>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ILifeService, LifeService>();
builder.Services.AddScoped<ILifeRepository, LifeRepository>();
builder.Services.AddScoped<IComicRepository, ComicRepository>();
builder.Services.AddScoped<IComicService, ComicService>();

builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddAutoMapper(typeof(Program));


// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<JwtMiddleware>();
// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
