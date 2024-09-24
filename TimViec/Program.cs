using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimViec;
using TimViec.Data;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
 .AddDefaultTokenProviders()
 .AddDefaultUI()
 .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICompanyRespository, EFCompanyRespository>();
builder.Services.AddScoped<IJobRespository, EFJobRespository>();
builder.Services.AddScoped<IStatusRepository, EFStatusJobRepository>();
builder.Services.AddScoped<IApplicationUser, EFApplicationUser>();
builder.Services.AddScoped<IRankRespository, EFRankRespository>();
builder.Services.AddScoped<ICityRespository, EFCityRespository>();
builder.Services.AddScoped<IType_WorkRespository, EFType_WorkRespository>();
builder.Services.AddScoped<ISkillRespository, EFSkillRespository>();
builder.Services.AddScoped<IfavouriteJob, EFfavouriteJob>();
builder.Services.AddScoped<IfeedbackRepository, EFfeedbackRepository>();

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
app.MapRazorPages();   

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "Admin",
	  pattern: "{area:exists}/{controller=Manager}/{action=Index}/{id?}"
	);

	endpoints.MapControllerRoute(
	 name: "Company",
	 pattern: "{area:exists}/{controller=Company}/{action=Index}/{id?}"
   );

    endpoints.MapGet("/TestGmail", async (context) =>
	{
		var message = await SendMail.SendGmail("nhathaoha11@gmail.com", "yukunvip21@gmail.com", "hello", "Xin chao", "nhathaoha11@gmail.com", "gheh wppp gokl rmrn");

		await context.Response.WriteAsync(message);
    });
});

app.MapWhen(context => context.Request.Path.Value.Contains("hello"), builder =>
{
	builder.Run(async context =>
	{
		await context.Response.WriteAsync("Nhat Hao");
	});
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
