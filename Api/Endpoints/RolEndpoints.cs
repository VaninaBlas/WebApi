using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints
{
    public static class RolEndpoints
    {
        public static RouteGroupBuilder MapRolEndpoints(this RouteGroupBuilder app)
        {
            List<Rol> roles=[
            new Rol {IdRol=1, Nombre="Preceptor", Habilitado=true, FechaCreacion= DateTime.Parse("2024-09-20")},
            new Rol {IdRol=2, Nombre="Alumno", Habilitado=true, FechaCreacion= DateTime.Parse("2024-07-20")},
            new Rol {IdRol=3, Nombre="Profesor", Habilitado=true, FechaCreacion= DateTime.Parse("2024-08-20")}
            ];
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

            });
                

            app.MapGet("/roles", ()=>{
                return Results.Ok(roles);
            });
            
            app.MapGet("/rol", ([FromQuery] int idRol)=>{
                var rolAEspecifico = roles.FirstOrDefault(rol => rol.IdRol == idRol);
                if (rolAEspecifico != null)
                {
                    return Results.Ok(rolAEspecifico); //Codigo 200
                }
                else
                {
                    return Results.NotFound(); //Codigo 404
                }
            });

            app.MapPut("/rol", ([FromQuery] int idRol, [FromBody] Rol rol)=>{
                
                var rolAActualizar = roles.FirstOrDefault(rol => rol.IdRol == idRol);
                if(rolAActualizar == null)
                    return Results.NotFound();
                if(rolAActualizar.Nombre != rol.Nombre)
                    return Results.BadRequest();

                rolAActualizar.Habilitado=rol.Habilitado;
                return Results.Ok(roles);
            });

            app.MapDelete("/rol", ([FromQuery] int idRol)=>{
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
            });
            return app;
        }      
    }
}