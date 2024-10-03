using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static RouteGroupBuilder MapUsuarioEndpoints(this RouteGroupBuilder app)
        {
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
                    return Results.Created(); // Codigo 201
                }
                else
                {
                    return Results.BadRequest(); // Codigo 400
                }

            });

            app.MapGet("/usuarios", () =>{
                return Results.Ok(usuarios);
            });

            //Obtener usuario por id
            app.MapGet("/usuario", ([FromQuery] int idUsuario) =>
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
            });

            app.MapPut("/usuario", ([FromQuery] int idUsuario, [FromBody] Usuario usuario) =>
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

            });

            app.MapDelete("/usuario", ([FromQuery] int idUsuario) =>
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
            });
            return app;

        }
    }
}