using Api;
using Microsoft.AspNetCore.Mvc;
using Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    options.EnableTryItOutByDefault()
    );
}
// Registros
List<Rol> roles=[
    new Rol {IdRol=1, Nombre="Preceptor", Habilitado=true, FechaCreacion= DateTime.Parse("2024-09-20")},
    new Rol {IdRol=2, Nombre="Alumno", Habilitado=true, FechaCreacion= DateTime.Parse("2024-07-20")},
    new Rol {IdRol=3, Nombre="Profesor", Habilitado=true, FechaCreacion= DateTime.Parse("2024-08-20")}
];
List <Usuario> usuarios=[
    new Usuario {IdUsuario=1, Nombre="Vanina", Email="vanyabrilconblas@gmail.com", NombreUsuario="VaninaBlas", Contraseña="chilipicante", Habilitado=true, FechaCreacion= DateTime.Parse("2024-09-22")},
    new Usuario {IdUsuario=2, Nombre="Priscila", Email="pri@gmail.com", NombreUsuario="Pri", Contraseña="ex", Habilitado=true, FechaCreacion= DateTime.Parse("2024-09-16")},
    new Usuario {IdUsuario=3, Nombre="Jasmin", Email="jasmin@gmail.com", NombreUsuario="Jas", Contraseña="veinticinco,veintiuno", Habilitado=true, FechaCreacion= DateTime.Parse("2024-05-12")}
];

// asignar y designar un rol a un usuario y un usuario a un rol
// rol a un usuario
app.MapPost("/rol/{idRol}/usuario/{idUsuario}", (int idRol, int idUsuario)=>{
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);

    if (usuario != null && rol != null)
    {
        rol.Usuarios.Add(usuario);
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("/rol/{idRol}/usuario/{idUsuario}", (int idRol, int idUsuario)=>{
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);

    if (usuario != null && rol != null )
    {
        rol.Usuarios.Remove(usuario);
        return Results.Ok();
    }

    return Results.NotFound();
});

// usuario a un rol
app.MapPost("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, int idRol)=>{
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);  
    if (usuario != null && rol != null)
    {
        usuario.Roles.Add(rol);
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, int idRol)=>{
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    if (usuario != null && rol != null )
    {
        usuario.Roles.Remove(rol);
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapGroup("/api")
    .MapUsuarioEndpoints()
    .WithTags("Usuario");

app.MapGroup("/api")
    .MapRolEndpoints()
    .WithTags("Rol");
app.Run();
