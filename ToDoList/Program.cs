using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskmaster.Mappings;
using ToDoList.Configuration;
using ToDoList.DataAccess.Context;
using ToDoList.DataAccess.Model;
using ToDoList.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind("StyleConfig", new StyleConfig());

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddSession();

builder.Services.AddDbContext<ToDoContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ToDoContext>();

builder.Services.ConfigureApplicationCookie(option =>
{
	option.Cookie.Name = "ToDoListWebAppCookie";
	option.LoginPath = "/account/login";
});

builder.Services.AddMvc(options =>
{
	options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
}).AddSessionStateTempDataProvider();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();