using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Persistence;
using TeachersSideAPI.Service;
using TeachersSideAPI.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TeachersSideContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        @"User ID = postgres;
        Password=my_password;
        Server=localhost;
        Port=54322;
        Database=TeachersSideDB;
        Integrated Security=true;
        Pooling=true"
    )
);

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped(typeof(UserManager<>));

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