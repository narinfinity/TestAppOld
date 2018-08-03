using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TestApp.Core.Entity.App;
using TestApp.Infrastructure.Dependency;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Rewrite;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using TestApp.Web.Models.Authorization.Handler;
using TestApp.Web.Models.Authorization.Requirement;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;
using OpenIddict.Core;
using Newtonsoft.Json.Converters;
using JSNLog;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.IO;
using NSwag.AspNetCore;
using NJsonSchema;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace TestApp.Web
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

      if (env.IsDevelopment())
      {
        // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
        builder.AddUserSecrets<Startup>();
      }

      builder.AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IContainer AppContainer;
    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services.AddDbContext(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        //options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        options.UseOpenIddict<string>();
      });

      //Adds cookies and sets the default authenticate and challenge schemes to the app - cookie IdentityConstants.ApplicationScheme
      //  sets the default sign -in scheme to the external cookie IdentityConstants.ExternalScheme
      services.AddIdentity<User, IdentityRole<string>>(options =>
      {
        //Signin settings
        //options.SignIn.RequireConfirmedEmail = true;
        //options.SignIn.RequireConfirmedPhoneNumber = false;

        //Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 6;

        //Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.AllowedForNewUsers = true;

        //User settings
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

        //Configure Identity to use the same JWT claims as OpenIddict instead
        //of the legacy WS - Federation claims it uses by default(ClaimTypes),
        // which saves you from doing the mapping in your authorization controller.
        options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
        options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
        options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
      })
      .AddEFStores()
      //tokens for reset passwords, change email and change telephone number operations, and for two factor authentication
      .AddDefaultTokenProviders();
      //Optionally invoke the ConfigureApplicationCookie or ConfigureExternalCookie to tweak the Identity cookie settings in AddIdentity<>
      services.ConfigureApplicationCookie(options =>
      {
        // Cookie settings
        //options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Expiration = TimeSpan.FromMinutes(60);
        options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
        options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
        options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
        options.SlidingExpiration = true;//new cookie will be issued with a new expiration time
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

        options.Events.OnValidatePrincipal = context =>
              {
                //if (context.Principal != null && !context.HttpContext.User.Claims.Any())
                //{
                //    context.HttpContext.User = context.Principal;
                //    context.RejectPrincipal();
                //    //want to non-destructively update the user principal
                //    context.ReplacePrincipal(new ClaimsPrincipal(context.HttpContext.User.Identity));
                //    //and set the context.ShouldRenew property to true
                //    context.ShouldRenew = true;
                //}                    
                return Task.CompletedTask;
              };
        options.Events.OnRedirectToAccessDenied = context =>
              {

                return Task.CompletedTask;
              };
      });
      /**
      services.ConfigureExternalCookie(options =>
      {
          // Cookie settings
          options.Cookie.Name = IdentityConstants.ExternalScheme;
          options.Cookie.HttpOnly = true;
          options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
          options.Cookie.SameSite = SameSiteMode.Lax;
          options.Cookie.Expiration = TimeSpan.FromMinutes(60);
          options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
          options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
          options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
          options.SlidingExpiration = true;
          options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
      });
      **/

      // Register the OpenIddict services.
      // Note: use the generic overload if you need
      // to replace the default OpenIddict entities.
      services.AddOpenIddict<string>(options =>
      {
        // Register the Entity Framework stores.
        DependencyServiceExtensions.AddEFCoreStores(options);

        // Register the ASP.NET Core MVC binder used by OpenIddict.
        // Note: if you don't call this method, you won't be able to
        // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
        options.AddMvcBinders();

        // Enable the authorization, logout, token and userinfo endpoints.
        options.EnableAuthorizationEndpoint("/connect/authorize")
               .EnableLogoutEndpoint("/connect/logout")
               .EnableTokenEndpoint("/connect/token")
               .EnableUserinfoEndpoint("/api/userinfo");

        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(20));
        options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(20160));

        // Note: the Mvc.Client sample only uses the code flow and the password flow, but you
        // can enable the other flows if you need to support implicit or client credentials.
        options.AllowAuthorizationCodeFlow()
               //.AllowImplicitFlow()
               .AllowPasswordFlow()
               //.AllowClientCredentialsFlow()
               //.AllowRefreshTokenFlow()
               ;

        // Mark the "profile" scope as a supported scope in the discovery document.
        options.RegisterScopes(new[] {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Address,
                    OpenIdConnectConstants.Scopes.Phone,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles
          });
        options.RegisterClaims(new[] {
                    OpenIdConnectConstants.Claims.ClientId,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Subject,
                    OpenIdConnectConstants.Claims.Role,
                    OpenIdConnectConstants.Claims.Gender,
                    OpenIdConnectConstants.Claims.Profile,
                    OpenIdConnectConstants.Claims.Email,
                    OpenIdConnectConstants.Claims.EmailVerified,
                    OpenIdConnectConstants.Claims.PhoneNumber,
                    OpenIdConnectConstants.Claims.PhoneNumberVerified,
                    OpenIdConnectConstants.Claims.Nonce,
                    OpenIdConnectConstants.Claims.Locale,
                    OpenIdConnectConstants.Claims.Zoneinfo,
          });
        // Make the "client_id" parameter mandatory when sending a token request.
        options.RequireClientIdentification();

        // When request caching is enabled, authorization and logout requests
        // are stored in the distributed cache by OpenIddict and the user agent
        // is redirected to the same page with a single parameter (request_id).
        // This allows flowing large OpenID Connect requests even when using
        // an external authentication provider like Google, Facebook or Twitter.
        options.EnableRequestCaching();

        // On production, using a X.509 certificate stored in the machine store is recommended.
        // You can generate a self-signed certificate using Pluralsight's self-cert utility:
        // https://s3.amazonaws.com/pluralsight-free/keith-brown/samples/SelfCert.zip
        //
        options.AddSigningCertificate("7D2A741FE34CC2C7369237A5F2078988E17A6A75");

        // Alternatively, you can also store the certificate as an embedded.pfx resource
        // directly in this assembly or in a file published alongside this project:
        //options.AddSigningCertificate(
        //    assembly: typeof(Startup).GetTypeInfo().Assembly,
        //    resource: "Certificate.pfx",
        //    password: "OpenIddict");

        // Use JWT access tokens instead of the default
        // encrypted format, the following lines are required:                
        options.UseJsonWebTokens();

        // Register a new ephemeral key, that is discarded when the application
        // shuts down. Tokens signed using this key are automatically invalidated.
        // This method should only be used during development.
        //options.AddEphemeralSigningKey();

        // During development, you can disable the HTTPS requirement.
        options.DisableHttpsRequirement();

      });


      // If don't want the cookie to be automatically authenticated and assigned to HttpContext.User, 
      // remove the CookieAuthenticationDefaults.AuthenticationScheme parameter
      //CookieAuthenticationDefaults.AuthenticationScheme,JwtBearerDefaults.AuthenticationScheme
      /**services.AddAuthentication(options =>
      {
          options.DefaultScheme = IdentityConstants.ApplicationScheme;
          options.DefaultSignInScheme = IdentityConstants.ExternalScheme;

          // user to be automatically signed in
          // or want to use[Authorize] or Policies without schemes
          // not needed if AddIdentity is called before
          options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          // when we need the user to login, we will be using the OpenID Connect scheme
          options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
          //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

          //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      });
      .AddCookie(options =>
      //{
      //    options.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
      //    options.Cookie.Domain = "http://localhost";
      //    options.Cookie.Path = "/";
      //    options.Cookie.HttpOnly = true;
      //    options.Cookie.Expiration = TimeSpan.FromMinutes(60);
      //    options.SlidingExpiration = true;//new cookie will be issued with a new expiration time
      //    options.LoginPath = "/Account/LogIn";
      //    options.LogoutPath = "/Account/LogOut";
      //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
      //    options.Cookie.SameSite = SameSiteMode.Lax;//SameSiteMode.Strict - breaks OAuth2 and other cross-origin authentication schemes
      //    options.EventsType = typeof(CustomCookieAuthenticationEvents);
      //    services.AddScoped<CustomCookieAuthenticationEvents>();

      //    options.Events.OnRedirectToLogin = context =>
      //    {
      //        context.Response.Headers["Location"] = context.RedirectUri;
      //        context.Response.StatusCode = 401;
      //        return Task.CompletedTask;
      //    };
      //})**/


      // If prefer using JWT, don't forget to disable the automatic
      // JWT -> WS-Federation claims mapping used by the JWT middleware:
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
      JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
      services
      .AddAuthentication(options =>
      {
        options.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
      })
      .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
      //.AddOAuthValidation()// Register the OAuth2 validation handler. 
      .AddJwtBearer(options =>
      {
        options.Authority = "https://localhost:44397";
        options.Audience = "resource_server"; //WebApi Server is Resource Server
                                              //options.SaveToken = true;
        options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
          //SaveSigninToken = true,

          NameClaimType = OpenIdConnectConstants.Claims.Subject,
          RoleClaimType = OpenIdConnectConstants.Claims.Role,
          //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value))
        };
        /**options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = ctx =>
            {
                var p = ctx;

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = ctx =>
            {
                var p = ctx;

                return Task.CompletedTask;
            },
            OnChallenge = ctx =>
            {
                var p = ctx;

                return Task.CompletedTask;
            },
            OnTokenValidated = ctx =>
            {
                var p = ctx;

                return Task.CompletedTask;
            }
        };**/
      })
      .AddOpenIdConnect(options =>
      {
        options.ClientId = Configuration["OpenIdConnect:ClientId"];
        options.ClientSecret = Configuration["OpenIdConnect:ClientSecret"];

        options.Authority = Configuration["OpenIdConnect:Authority"];
        options.ResponseType = Configuration["OpenIdConnect:ResponseType"];
        options.ResponseMode = Configuration["OpenIdConnect:ResponseMode"];

        options.CallbackPath = Configuration["OpenIdConnect:RedirectUri"];
        options.RemoteSignOutPath = Configuration["OpenIdConnect:PostLogoutRedirectUri"];
        options.Resource = Configuration["OpenIdConnect:Resource"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
          //SaveSigninToken = true,
          NameClaimType = OpenIdConnectConstants.Claims.Subject,
          RoleClaimType = OpenIdConnectConstants.Claims.Role,
          //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value))
        };
        options.Events.OnAuthenticationFailed = context =>
              {

                return Task.CompletedTask;
              };
        options.Events.OnRemoteFailure = context =>
              {

                return Task.CompletedTask;
              };
        options.GetClaimsFromUserInfoEndpoint = true;
        options.RequireHttpsMetadata = true;
      });
      /**
     //Add external authentication middleware below.To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
     .AddMicrosoftAccount(options =>
     {
         options.ClientId = Configuration["Microsoft:ClientId"];
         options.ClientSecret = Configuration["Microsoft:ClientSecret"];
     })            
     .AddFacebook(options =>
     {
         options.AppId = Configuration["Facebook:AppId"];
         options.AppSecret = Configuration["Facebook:AppSecret"];
     })
     .AddGoogle(options =>
     {
         options.ClientId = Configuration["Google:ClientId"];
         options.ClientSecret = Configuration["Google:ClientSecret"];
     })
     .AddTwitter(options =>
     {
         options.ConsumerKey = Configuration["Twitter:ConsumerKey"];
         options.ConsumerSecret = Configuration["Twitter:ConsumerSecret"];
     });
     
     **/

      // if resource server is separated from the authorization server. use backchannel HTTP calls to validate the incoming tokens
      //services.AddAuthentication()
      //.AddOAuthIntrospection(options =>
      //{
      //    options.Authority = new Uri("https://localhost:44397");
      //    options.Audiences.Add("resource_server");
      //    options.ClientId = "resource_server";
      //    options.ClientSecret = "875sqd4s5d748z78z7ds1ff8zz8814ff88ed8ea4z4zzd";
      //    options.RequireHttpsMetadata = true;
      //});

      // Allowing anonymous users into app at the IIS or HTTP.sys layer but authorizing users at the Controller level
      //services.AddAuthentication(IISDefaults.AuthenticationScheme);

      services.AddAntiforgery(options =>
      {
        options.FormFieldName = "AntiforgeryFieldname";
        options.HeaderName = "X-CSRF-TOKEN-HEADER";
        //options.Cookie.Domain = "http://localhost";
        //options.Cookie.Name = "X-CSRF-TOKEN-COOKIE";
        //options.Cookie.Path = "/";
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;//whether SSL is required by the antiforgery                
        options.Cookie.SameSite = SameSiteMode.Lax;//SameSiteMode.Strict - breaks OAuth2 and other cross-origin authentication schemes
        options.SuppressXFrameOptionsHeader = false;//X-Frame-Options: by default "SAMEORIGIN" ("DENY", "ALLOW-FROM https://example.com/")

      });

      var issuer = Configuration["JwtBearer:Authority "];
      services.AddAuthorization(options =>
      {
        options.AddPolicy("Founders", policy => policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5"));//[Authorize(Policy = "Founders")]
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));//[Authorize(Policy = "EmployeeOnly")]
        options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator")); //[Authorize(Policy = "RequireAdministratorRole")]
        options.AddPolicy("ElevatedRights", policy => policy.RequireRole("Administrator", "PowerUser", "BackupAdministrator")); //[Authorize(Policy = "ElevatedRights")]
        options.AddPolicy("Reader", policy => policy.Requirements.Add(new HasScopeRequirement("read", issuer)));//[Authorize(Policy = "Reader")]
        options.AddPolicy("Creator", policy => policy.Requirements.Add(new HasScopeRequirement("create", issuer)));//[Authorize(Policy = "Creator")]
        options.AddPolicy("BadgeEntry", policy => //[Authorize(Policy = "BadgeEntry")]
                          policy.RequireAssertion(context =>
                          {
                            if (context.Resource is Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext mvcContext)
                            {
                              // Examine MVC specific things like routing data.
                              var authnType = mvcContext.HttpContext.User.Identity.AuthenticationType;
                            }
                            return context.User.HasClaim(c =>
                                        (c.Type == ClaimTypes.Name || c.Type == ClaimTypes.Email)
                                        && c.Issuer == "https://microsoftsecurity.com" && c.Value == "testuser");
                          }));
        options.AddPolicy("Over21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));//[Authorize(Policy="Over21")]
        options.AddPolicy("EditPolicy", policy => policy.Requirements.Add(new SameAuthorRequirement()));

        options.AddPolicy("CustomApiAuthPolicy", policy =>
              {
                //policy.AuthenticationSchemes.Add(IdentityServerConstants.ExternalCookieAuthenticationScheme);
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.AuthenticationSchemes.Add(OpenIdConnectDefaults.AuthenticationScheme);
                //policy.AuthenticationSchemes.Add(IdentityConstants.ApplicationScheme);
                //policy.AuthenticationSchemes.Add(IdentityConstants.ExternalScheme);
                //policy.AuthenticationSchemes.Add(CookieAuthenticationDefaults.AuthenticationScheme);//[Authorize(AuthenticationSchemes = AuthSchemes)]
                policy.Requirements.Add(new MyLoggingAuthorizationRequirement());
                //policy.Requirements.Add(new MinimumAgeRequirement(18));
                //policy.Requirements.Add(new HasScopeRequirement("read", issuer));
                //policy.Requirements.Add(new HasScopeRequirement("create", issuer));
              });

      });
      //multiple handlers for a single requirement if want evaluation to be on an OR basis
      //all handlers for a requirement will be called when a policy requires the requirement
      //allows requirements to have side effects, such as logging, which will always take place even if context.Fail() has been called in another handler
      services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
      services.AddSingleton<IAuthorizationHandler, BadgeEntryHandler>();
      services.AddSingleton<IAuthorizationHandler, HasTemporaryStickerHandler>();
      services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
      //An instance of the handler will be created, DI will inject the registered ILoggerFactory into your constructor
      services.AddSingleton<IAuthorizationHandler, LoggingAuthorizationHandler>();
      services.AddSingleton<IAuthorizationHandler, DocumentAuthorizationHandler>();
      services.AddSingleton<IAuthorizationHandler, DocumentAuthorizationCrudHandler>();


      /*
        Safe list ranges are specified as Unicode code charts, not languages. 
        The Unicode standard has a list of code charts you can use to find the chart containing your characters. 
        Each encoder, Html, JavaScript and Url, must be configured separately.
     */
      services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs }));
      services.AddCors();
      services.AddMvc(options =>
      {
        //options.Filters.Add(new RequireHttpsAttribute());
        //options.Filters.Add(new AddHeaderAttribute("GlobalAddHeader", "Result filter added to MvcOptions.Filters")); // an instance
        //options.Filters.Add(typeof(SampleActionFilter)); // by type
        //options.Filters.Add(new SampleGlobalActionFilter()); // an instance
        //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

        // add custom binder to beginning of collection
        //options.ModelBinderProviders.Insert(0, new AuthorEntityBinderProvider());
      })
      .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
      .AddControllersAsServices()
      //.ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericControllerFeatureProvider()))
      .AddJsonOptions(options =>
      {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      })
      .AddXmlSerializerFormatters();

      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
      });


      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info
        {
          Title = "My API V1",
          Version = "v1",
          Description = "A simple example ASP.NET Core Web API",
          TermsOfService = "None",
          Contact = new Contact
          {
            Name = "Shayne Boyer",
            Email = string.Empty,
            Url = "https://twitter.com/spboyer"
          },
          License = new License
          {
            Name = "Use under LICX",
            Url = "https://example.com/license"
          }
        });
        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });



      //services.AddSingleton<IAuthorizationHandler, HasScopeRequirement>();
      /* Avoid static access to services.
       * Avoid service location in your application code.
       * Avoid static access to HttpContext.
       */
      return services.AddExternalDependencyServiceProvider(out AppContainer);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      var options = new RewriteOptions().AddRedirectToHttps(301, 5001);//301 (Moved Permanently) and port: 5001
                                                                       //app.UseRewriter(options);

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        //app.UseBrowserLink();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true,

        });
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      // Configure JSNLog
      var jsnlogConfiguration = new JsnlogConfiguration();
      app.UseJSNLog(new LoggingAdapter(loggerFactory), jsnlogConfiguration);

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSpaStaticFiles();

      app.UseCors(builder =>
      {
        builder
              .WithExposedHeaders("WWW-Authenticate")
              .WithOrigins("https://localhost:44397")
              .WithMethods("GET", "POST")
              .WithHeaders("Authorization");
        // implisitFlow
        //builder.WithMethods("GET");
        //builder.WithHeaders("Authorization");
        // refreshFlow
        //builder.AllowAnyHeader();
        //builder.AllowAnyMethod();
      });

      //Call the UseAuthentication method before calling AddMvc
      //Invoke the Authentication Middleware that sets the HttpContext.User property
      app.UseAuthentication();//app.UseIdentity();

      //Swashbuckle: Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();
      //NSwag Register the Swagger generator
      app.UseSwagger(typeof(Startup).Assembly, settings =>
      {
        settings.PostProcess = document =>
        {
          document.Info.Version = "v1";
          document.Info.Title = "ToDo API";
          document.Info.Description = "A simple ASP.NET Core web API";
          document.Info.TermsOfService = "None";
          document.Info.Contact = new NSwag.SwaggerContact
          {
            Name = "Shayne Boyer",
            Email = string.Empty,
            Url = "https://twitter.com/spboyer"
          };
          document.Info.License = new NSwag.SwaggerLicense
          {
            Name = "Use under LICX",
            Url = "https://example.com/license"
          };
        };
      });
      //Swashbuckle: Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        // To serve the Swagger UI at the app's root (http://localhost:<port>/), set the RoutePrefix property to an empty string.
        // c.RoutePrefix = string.Empty;
      });

      //NSwag: Enable the Swagger UI middleware and the Swagger generator
      app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
      {
        settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
      });

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapSpaFallbackRoute(
                  name: "spa-fallback",
                  defaults: new { controller = "Home", action = "Index" });
      });



      // If you want to dispose of resources that have been resolved in the
      // application container, register for the "ApplicationStopped" event.
      appLifetime.ApplicationStopped.Register(() => AppContainer?.Dispose());

      // Seed the database, should be part of a setup script.
      //app.ApplicationServices.InitializeAsync(CancellationToken.None).GetAwaiter().GetResult();
    }


  }
}
