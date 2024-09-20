using Api;
using Microsoft.AspNetCore.Mvc;

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
    app.UseSwaggerUI();
}
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

app.MapPost("/usuario", ([FromBody] Usuario usuario)=>
{
    if(usuario.Nombre != null && usuario.Nombre != string.Empty && usuario.Email !=null && usuario.Email !=string.Empty && usuario.Contraseña != null && usuario.Contraseña != string.Empty && usuario.NombreUsuario != null && usuario.NombreUsuario != string.Empty)
    {
        usuarios.Add(usuario);
        return Results.Ok(usuarios);
    }
    else
    {
        return Results.BadRequest();
    }

})
    .WithTags("Usuario");
app.MapGet("/usuarios", () =>{
    return Results.Ok(usuarios);
})
    .WithTags("Usuario");

app.MapGet("/usuario/{idUsuario}", ([FromQuery] int idUsuario) =>
{
    var usuarioAEspecifico = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    if (usuarioAEspecifico != null)
    {
        return Results.Ok(usuarioAEspecifico); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Usuario");
app.MapPut("/usuario/{idUsuario}", ([FromQuery] int idUsuario, [FromBody] Usuario usuario) =>
{
    var usuarioAActualizar = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    if(usuarioAActualizar.Nombre != usuario.Nombre)
        return Results.BadRequest();
    if(usuarioAActualizar != null){
        usuarioAActualizar.Contraseña = usuario.Contraseña;
        usuarioAActualizar.Email= usuario.Email;
        usuarioAActualizar.NombreUsuario= usuario.NombreUsuario;
        return Results.Ok(usuarios);
    }
    else
    {
        return Results.NotFound();
    }
    

})
    .WithTags("Usuario");
app.Run();
