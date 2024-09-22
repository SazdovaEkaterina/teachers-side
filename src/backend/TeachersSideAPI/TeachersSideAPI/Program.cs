using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeachersSideAPI.Domain;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Persistence.Repositories.Implementation;
using TeachersSideAPI.Service;
using TeachersSideAPI.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(x => 
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TeachersSideContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        @"User ID = postgres;
        Password=my_password;
        Server=localhost;
        Database=TeachersSideDB;
        Integrated Security=true;
        Pooling=true"
    )
);

builder.Services.AddDefaultIdentity<Teacher>(
        options =>
        {
             options.SignIn.RequireConfirmedAccount = true;
             options.User.RequireUniqueEmail = true;
        })
    .AddEntityFrameworkStores<TeachersSideContext>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (
                    Encoding.ASCII.GetBytes
                    (
                        builder.Configuration["Authentication:SecretForKey"]
                    )
                )
            };
        }
    );

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IForumRepository, ForumRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IForumService, ForumService>();
builder.Services.AddTransient<IMaterialService, MaterialService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();

builder.Services.AddTransient<IJwtSecurityTokenGenerator, JwtSecurityTokenGenerator>();

var env = builder.Environment;

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new MapperProfile(env.ContentRootPath)); 
});

builder.Services.AddScoped(typeof(UserManager<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:4200"));

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
