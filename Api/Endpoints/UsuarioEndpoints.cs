using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static RouteGroupBuilder MapUsuarioEndpoints(this RouteGroupBuilder app)
        {

            app.MapPost("/usuario", ([FromBody] Usuario usuario, EscuelaContext context)=>
            {
                if(usuario.Nombre != null && usuario.Nombre != string.Empty && usuario.Email !=null && usuario.Email !=string.Empty && usuario.Contrasena != null && usuario.Contrasena != string.Empty && usuario.NombreUsuario != null && usuario.NombreUsuario != string.Empty)
                {
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                    return Results.Created(); // Codigo 201
                }
                else
                {
                    return Results.BadRequest(); // Codigo 400
                }

            });

            app.MapGet("/usuarios", (EscuelaContext context) =>{
                return Results.Ok(context.Usuarios);
            });

            //Obtener usuario por id
            app.MapGet("/usuario", ([FromQuery] int idUsuario, EscuelaContext context) =>
            {
                var usuarioAEspecifico = context.Usuarios.FirstOrDefault(usuario => usuario.Idusuario == idUsuario);
                if (usuarioAEspecifico != null)
                {
                    return Results.Ok(usuarioAEspecifico); //Codigo 200
                }
                else
                {
                    return Results.NotFound(); //Codigo 404
                }
            });

            app.MapPut("/usuario", ([FromQuery] int idUsuario, [FromBody] Usuario usuario, EscuelaContext context) =>
            {
                var usuarioAActualizar = context.Usuarios.FirstOrDefault(usuario => usuario.Idusuario == idUsuario);
                if(usuarioAActualizar == null)
                    return Results.NotFound();        
                if(usuarioAActualizar.Nombre != usuario.Nombre)
                    return Results.BadRequest();

                usuarioAActualizar.Contrasena = usuario.Contrasena;
                usuarioAActualizar.Email= usuario.Email;
                usuarioAActualizar.NombreUsuario= usuario.NombreUsuario;
                context.SaveChanges();
                return Results.Ok(context.Usuarios);

            });

            app.MapDelete("/usuario", ([FromQuery] int idUsuario, EscuelaContext context) =>
            {
                var usuarioAEliminar = context.Usuarios.FirstOrDefault(usuario => usuario.Idusuario == idUsuario);
                if (usuarioAEliminar != null)
                {
                    context.Usuarios.Remove(usuarioAEliminar);
                    context.SaveChanges();  
                    return Results.NoContent(); //Codigo 204
                }
                else
                {
                    return Results.NotFound(); //Codigo 404
                }
            });
            // usuario a un rol
            app.MapPost("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, Guid idRol, EscuelaContext context)=>{
                var rol = context.Rols.FirstOrDefault(rol => rol.Idrol == idRol);
                var usuario = context.Usuarios.FirstOrDefault(usuario => usuario.Idusuario == idUsuario);  
                if (usuario != null && rol != null)
                {
                    context.Usuariorols.Add(new Usuariorol{Idusuariorol =0, IdrolNavigation= rol, IdusuarioNavigation= usuario});
                    context.SaveChanges();
                    return Results.Ok();
                }

                return Results.NotFound();
            });

            app.MapDelete("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, Guid idRol, EscuelaContext context)=>{
                var usuarioRol= context.Usuariorols.FirstOrDefault(x=> x.Idusuario == idUsuario && x.Idrol == idRol);
                if (usuarioRol is not null)
                {
                    context.Usuariorols.Remove(usuarioRol);
                    context.SaveChanges();
                    return Results.Ok();
                }

                return Results.NotFound();
            });
            return app;

        }
    }
}