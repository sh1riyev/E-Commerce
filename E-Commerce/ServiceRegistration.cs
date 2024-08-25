using FluentValidation.AspNetCore;
using System.Text;
using E_Commerce.Business.Interfaces;
using E_Commerce.Business.Services;
using E_Commerce.Core.Entities;
using E_Commerce.Data.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using E_Commerce.Business.Configratuions;
using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc.Routing;
using E_Commerce.Business.Mappings;
using E_Commerce.DTOs.CategoryDto;
using Microsoft.Extensions.Options;

namespace E_Commerce
{
    public static class ServiceRegistration
    {
        public static void Registration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(d => d.RegisterValidatorsFromAssemblyContaining<CreateCategoryDto>())
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:7052/");
                });
            });
            services.AddSignalR();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = "localhost:6379";
            });

            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("Default"),
                    opt =>

                        opt.MigrationsAssembly("E-Commerce.Data")
                    );
            });

            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;
                option.SignIn.RequireConfirmedPhoneNumber = false;
                option.SignIn.RequireConfirmedAccount = true;
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireLowercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = true;
                option.Lockout.AllowedForNewUsers = true;
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddAuthorization();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            services.AddHttpContextAccessor();
            services.Configure<DataProtectionTokenProviderOptions>(option =>
            {
                option.TokenLifespan = TimeSpan.FromMinutes(10);

            });
            services.AddAutoMapper(typeof(CategoryProfile).Assembly);
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ISendEmail, SendEmail>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<UrlHelperService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductCommentService, ProductCommentService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IBlogCommentService, BlogCommentService>();
            services.AddScoped<ICompaignsService, CompaignsService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAdressService, AdressService>();
            services.AddScoped<ICheckService, CheckService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
        }
    }
}

