using Azure.Core;
using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Inventario;
using Mezclas.API.Models.DTO.Usuario;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly AuthDbContext authDbContext;
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public UsuarioRepository(AuthDbContext authDbContext, MezclasOncologicasDbContext mezclasOncologicasDbContext, UserManager<IdentityUser> userManager)
        {
            this.authDbContext = authDbContext;
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
            this.userManager = userManager;
        }

        public async Task<ResponseModel> CreateUsuario(UsuarioDto model, string user)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                //matricula no existe
                if(await mezclasOncologicasDbContext.Cuenta.Where(x=>x.Matricula == model.Matricula.ToString()).CountAsync() > 0)
                {
                    rm.SetResponse(true, "La matricula ya se encuentra registrada.");
                    return rm;
                }

                //correo no existe
                if (await mezclasOncologicasDbContext.Cuenta.Where(x => x.CorreoElectronico == model.CorreoElectronico.ToString()).CountAsync() > 0)
                {
                    rm.SetResponse(true, "el correo electronico ya se encuentra registrada.");
                    return rm;
                }
                //si esde tipo quimico, cabina, inventario la unidad este permitida surtir mezclas
                //si es de tipo solicitante la unidad este permitida para solicitarr
                
                //generamos el usuario
                IdentityUser applicationUser = new IdentityUser();
                Guid guid = Guid.NewGuid();
                applicationUser.Id = guid.ToString();
                applicationUser.UserName = model.Matricula.ToString();
                applicationUser.Email = model.CorreoElectronico;
                applicationUser.NormalizedEmail = model.CorreoElectronico.ToUpper();
                applicationUser.NormalizedUserName = model.Matricula.ToString();
                applicationUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(applicationUser, model.Matricula.ToString());

                var rol = await this.authDbContext.Roles.Where(x=>x.Name == model.RolId).FirstOrDefaultAsync();

               
                //var roleMngr = new RoleManager<IdentityRole>(roleStore);

               // var roles = roleMngr.Roles.ToList();

                //generamos la cuenta
                Cuentum cuenta = new Cuentum()
                {
                    Id = guid,
                    Matricula = model.Matricula.ToString(),
                    Nombre = model.Nombre,
                    Apellidos = model.Apellidos,
                    CorreoElectronico = model.CorreoElectronico,
                    RolId = Guid.Parse(rol.Id),
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = Guid.Parse(user)
                };

                await userManager.CreateAsync(applicationUser);
                await userManager.AddToRoleAsync(applicationUser, rol.Name);
                await mezclasOncologicasDbContext.Cuenta.AddAsync(cuenta);
                await mezclasOncologicasDbContext.SaveChangesAsync();

                rm.result = cuenta;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
           
        }

        public async Task<ResponseModel> GetUsuarios()
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var usuarios = await this.authDbContext.Users.ToListAsync();
                
                rm.result = usuarios;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }
    }
}
