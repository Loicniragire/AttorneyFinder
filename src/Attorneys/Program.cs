var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.WebHost.UseUrls("http://*:80");

// Configure JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
	options.AddPolicy("User", policy => policy.RequireRole("User"));
	options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
});

// Add services for dependency injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAttorneyDataProvider, AttorneyDataRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<ITokenProvider, TokenProviderService>();
builder.Services.AddLogging(b => 
{
	b.AddConsole();
	b.AddDebug();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();  // Add controllers to the services

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Attorneys API"));
}

// Middleware
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthentication();  // Add authentication middleware
app.UseAuthorization();   // Add authorization middleware

app.UseMiddleware<CachingMiddleware>();  // Add caching middleware

app.MapControllers();  // Map controller routes

app.Run();

