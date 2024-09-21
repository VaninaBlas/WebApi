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

// Endpoints para usuario
app.MapPost("/usuario", ([FromBody] Usuario usuario)=>
{
    if(usuario.Nombre != null && usuario.Nombre != string.Empty && usuario.Email !=null && usuario.Email !=string.Empty && usuario.Contraseña != null && usuario.Contraseña != string.Empty && usuario.NombreUsuario != null && usuario.NombreUsuario != string.Empty)
    {
        usuarios.Add(usuario);
        return Results.Created(); // Codigo 201
    }
    else
    {
        return Results.BadRequest(); // Codigo 400
    }

})
    .WithTags("Usuario");
app.MapGet("/usuarios", () =>{
    return Results.Ok(usuarios);
})
    .WithTags("Usuario");

//Obtener usuario por id
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
    if(usuarioAActualizar == null)
        return Results.NotFound();        
    if(usuarioAActualizar.Nombre != usuario.Nombre)
        return Results.BadRequest();

    usuarioAActualizar.Contraseña = usuario.Contraseña;
    usuarioAActualizar.Email= usuario.Email;
    usuarioAActualizar.NombreUsuario= usuario.NombreUsuario;
    return Results.Ok(usuarios);

})
    .WithTags("Usuario");
app.MapDelete("/usuario/{idUsuario}", ([FromQuery] int idUsuario) =>
{
    var usuarioAEliminar = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    if (usuarioAEliminar != null)
    {
        usuarios.Remove(usuarioAEliminar);
        return Results.NoContent(); //Codigo 204
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Usuario");

// endpoints para Rol
app.MapPost("/rol", ([FromBody] Rol rol)=>{
    if(rol.Nombre != null && rol.Nombre != string.Empty)
    {
        roles.Add(rol);
        return Results.Created();
    }
    else
    {
        return Results.BadRequest();
    }

})
    .WithTags("Rol");

app.MapGet("/roles", ()=>{
    return Results.Ok(roles);
})
    .WithTags("Rol");
app.MapGet("/rol/{idRol}", ([FromQuery] int idRol)=>{
    var rolAEspecifico = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    if (rolAEspecifico != null)
    {
        return Results.Ok(rolAEspecifico); //Codigo 200
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Rol");
app.MapPut("/rol/{idRol}", ([FromQuery] int idRol, [FromBody] Rol rol)=>{
    
    var rolAActualizar = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    if(rolAActualizar == null)
        return Results.NotFound();
    if(rolAActualizar.Nombre != rol.Nombre)
        return Results.BadRequest();

    //consulta: segun lo de clase solo se deben actualizar los string excepto nombre pero para rol el unico string es nombre ¿?
    rolAActualizar.Habilitado=rol.Habilitado;
    return Results.Ok(roles);
})
    .WithTags("Rol");
app.MapDelete("/rol/{idRol}", ([FromQuery] int idRol)=>{
    var rolAEliminar = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    if (rolAEliminar != null)
    {
        roles.Remove(rolAEliminar);
        return Results.NoContent(); //Codigo 204
    }
    else
    {
        return Results.NotFound(); //Codigo 404
    }
})
    .WithTags("Rol");
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
})
    .WithTags("Rol");
app.MapDelete("/rol/{idRol}/usuario/{idUsuario}", (int idRol, int idUsuario)=>{
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);

    //consulta: que pasa si la relacion no existe? porque aca la elimina aunque no exista, deberia solo eliminar relaciones existentes
    if (usuario != null && rol != null )
    {
        rol.Usuarios.Remove(usuario);
        return Results.Ok();
    }

    return Results.NotFound();
})
    .WithTags("Rol");
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
})
    .WithTags("Usuario");
app.MapDelete("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, int idRol)=>{
    var rol = roles.FirstOrDefault(rol => rol.IdRol == idRol);
    var usuario = usuarios.FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
    if (usuario != null && rol != null )
    {
        usuario.Roles.Remove(rol);
        return Results.Ok();
    }

    return Results.NotFound();
})
    .WithTags("Usuario");
app.Run();
