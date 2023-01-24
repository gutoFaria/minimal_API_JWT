using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

// SERVIÇO DE AUTENTICAÇÃO
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt => {
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    }
);
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddDbContext<BlogDbContext>(
    opt => opt.UseSqlite(builder.Configuration.GetConnectionString("BlogConnection"))
);

builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IPostService,PostService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

#region LOGIN
app.MapPost("/login",async (UserLogin user,IUserService service) =>{
    if(!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
    {
        var loggedInUser = await service.GetAsync(user);
        if(loggedInUser is null) return Results.NotFound("User not found");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,loggedInUser.UserName),
            new Claim(ClaimTypes.Email,loggedInUser.EmailAddress),
            new Claim(ClaimTypes.GivenName,loggedInUser.GiveName),
            new Claim(ClaimTypes.Surname,loggedInUser.SurName),
            new Claim(ClaimTypes.Role,loggedInUser.Role),
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience:builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires:DateTime.UtcNow.AddDays(60),
            notBefore:DateTime.UtcNow,
            signingCredentials:new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                SecurityAlgorithms.HmacSha256
            )
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(tokenString);
    }
    return Results.NotFound();
});
#endregion

#region METODOS POSTS
app.MapPost("/createpost",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")]
async (Post post,IPostService service) => {
    var result = await service.CreatePost(post);
    return Results.Ok(result);
});
app.MapGet("/getpost/{id}", async (int id,IPostService service)=> {
    var result = await service.GetPostById(id);
    if(result is null)
        return Results.NotFound("Post not found");
    return Results.Ok(result);
});
app.MapGet("/getallposts",async (IPostService service) => {
    var result = await service.GetAllPost();
    
    return Results.Ok(result);
});
app.MapPut("/updatepost",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")]async(Post newPost,IPostService service) => {
    var updatePost = await service.UpdatePost(newPost);
    if(updatePost is null)
        return Results.NotFound("Post not found");
    return Results.Ok(updatePost);
});

app.MapDelete("/deletepost/{id}",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")]async(int id,IPostService service) =>{
    var result = await service.DeletePost(id);
    if(!result) Results.BadRequest("Somethingwent wrong");
    return Results.Ok(result);
});
#endregion

#region METODOS CATEGORY
app.MapGet("/getallcateories",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")] async (ICategoryService service) => {
    var result = await service.GetAllCategories();
    return Results.Ok(result);
});
app.MapGet("/getcategorybyid/{id}",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")] async(int id,ICategoryService service) => {
    var result = await service.GetCategoryById(id);
    if(result is null)
        return Results.NotFound("Category not found");
    return Results.Ok(result);
});

app.MapPost("/createcategory",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")] async(Category category, ICategoryService service) =>{
    var result = await service.CreateCategory(category);
    return Results.Ok(result);
});
app.MapPut("/updatecategory",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")] async(Category category,ICategoryService service) =>{
    var updateCategory = await service.UpdateCategory(category);
    if(updateCategory is null)
        return Results.NotFound("Category not found");
    return Results.Ok(updateCategory);
});
app.MapDelete("/deletecategory/{id}",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Administrator")] async(int id,ICategoryService service) =>{
    var result = await service.Deletecategory(id);
    if(!result) Results.BadRequest("Somethingwent wrong");
    return Results.Ok(result);
});
#endregion
app.UseHttpsRedirection();


app.Run();


