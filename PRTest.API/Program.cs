using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PRTest.Repository.DBContext;
using PRTest.Repository.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
	options.UseMemberCasing();
});

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//builder.Services.AddCors(options =>
//{
//	options.AddPolicy("Access-Control-Allow-Origin",
//		builder => builder.AllowAnyOrigin()
//				   .AllowAnyMethod()
//				   .AllowAnyHeader()
//	);

//});

builder.Services.AddDbContext<PRContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
	options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.IncludeErrorDetails = true;
	x.Events = new JwtBearerEvents()
	{
		OnMessageReceived = msg =>
		{
			var token = msg?.Request.Headers.Authorization.ToString();
			string path = msg?.Request.Path ?? "";
			if (!string.IsNullOrEmpty(token))

			{
				Console.WriteLine("Access token");
				Console.WriteLine($"URL: {path}");
				Console.WriteLine($"Token: {token}\r\n");
			}
			else
			{
				Console.WriteLine("Access token");
				Console.WriteLine("URL: " + path);
				Console.WriteLine("Token: No access token provided\r\n");
			}
			return Task.CompletedTask;
		}
	};
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SojibHossain")),
		ValidateAudience = false,
		ValidateIssuer = false,
		ClockSkew = TimeSpan.Zero
	};
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(x => x
	.AllowAnyMethod()
	.AllowAnyHeader()
	.SetIsOriginAllowed(origin => true) // allow any origin 
	.AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
