using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachersSideAPI.Persistence;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Persistence.Repositories.Implementation;
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

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IForumRepository, ForumRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped(typeof(UserManager<>));

builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IForumService, ForumService>();
builder.Services.AddTransient<IMaterialService, MaterialService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();

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