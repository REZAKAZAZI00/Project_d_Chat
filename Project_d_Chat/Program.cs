using Microsoft.AspNetCore.Authentication.Cookies;
using Project_d_Chat;
using Project_d_Chat.Core.Services;

var builder = WebApplication.CreateBuilder(args);
#region Authentication
//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(432000);

});

#endregion
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
#region DataBase Context
var connectionString = builder.Configuration.GetConnectionString("ChatConnection");
builder.Services.AddDbContext<ChatDbContext>(options => options.UseSqlServer(connectionString));
#endregion

#region IOC

builder.Services.AddTransient<IChatRoomService,ChatRoomService>();
builder.Services.AddTransient<IUserService,UserService>();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapHub<AgentHub>("/agentHub");
app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();
