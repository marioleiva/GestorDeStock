using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using SlnCibertec.Core.Requests;
using SlnCibertec.Core.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SlnCibertec.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICibertecContext _cibertecContext;

        public AccountController(IAccountService accountService, ICibertecContext cibertecContext)
        {
            _accountService = accountService;
            _cibertecContext = cibertecContext;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public ActionResult GetToken(GetTokenRequest request)
        {
            // 1. Validar el usuario
            var usuario = _accountService.ValidateUser(request.Username, request.Password);

            if (usuario == null)
            {
                // devolver un 401 (no autorizado)
                return StatusCode(401);
            }

            // 2. Crear la identidad del usuario (que viajará en el JWT)
            var identidad = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Name),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("DNI", usuario.Dni)
            };

            // 3. Crear los elementos para la encriptación
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("cibertec12345678"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Generar el token
            // 4.1. Obtener la fecha de expiración del token
            var tokenExpirationDate = DateTime.Now.AddSeconds(10);

            var token = new JwtSecurityToken(
                issuer: "Cibertec",
                audience: "app-react",
                claims: identidad,
                expires: tokenExpirationDate,
                signingCredentials: credenciales
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // 5. Crear el refresh token
            var refreshToken = new RefreshToken
            {
                ExpiresAt = tokenExpirationDate,
                UserId = usuario.Id,
                Token = Guid.NewGuid().ToString()
            };

            // 5.1. Guardar en BD
            _cibertecContext.RefreshTokens.Add(refreshToken);
            var resultado = _cibertecContext.Commit();

            // 6. Retornar el token en un objeto JSON basado en la clase GetTokenResponse
            return Ok(new GetTokenResponse { AccessToken = jwtToken, RefreshToken = refreshToken.Token, ExpiresIn = 10 });
        }

        [HttpPost("token/refresh")]
        [AllowAnonymous]
        public ActionResult RefreshToken(RefreshTokenRequest request)
        {
            // 1. Obtener el registro de la BD
            var refreshToken = _cibertecContext.RefreshTokens.FirstOrDefault(r => r.Token == request.RefreshToken);

            if (refreshToken == null)
            {
                // develover 401
                return StatusCode(401);
            }

            // 1.1 Validar la fecha de expiración
            // TODO


            // 2. Obtener el usuario
            var usuario = _cibertecContext.Users.Find(refreshToken.UserId);

            if (usuario == null)
            {
                // devolver un 401 (no autorizado)
                return StatusCode(401);
            }

            // 2. Crear la identidad del usuario (que viajará en el JWT)
            var identidad = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Name),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("DNI", usuario.Dni)
            };

            // 3. Crear los elementos para la encriptación
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("cibertec12345678"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Generar el token
            // 4.1. Obtener la fecha de expiración del token
            var tokenExpirationDate = DateTime.Now.AddSeconds(10);

            var token = new JwtSecurityToken(
                issuer: "Cibertec",
                audience: "app-react",
                claims: identidad,
                expires: tokenExpirationDate,
                signingCredentials: credenciales
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // 5. Actualizar el refresh token
            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.ExpiresAt = tokenExpirationDate;

            // 5.1. Guardar cambios en BD
            var resultado = _cibertecContext.Commit();

            // 6. Retornar el token en un objeto JSON basado en la clase GetTokenResponse
            return Ok(new GetTokenResponse { AccessToken = jwtToken, RefreshToken = refreshToken.Token, ExpiresIn = 10 });
        }

        [HttpGet]
        [Authorize]
        public ActionResult ObtenerTodos()
        {
            return Ok(_cibertecContext.Users);
        }

        [HttpGet("{Id}")]
        public ActionResult ObtenerPorId(int Id)
        {
            return Ok(_cibertecContext.Users.Find(Id));
        }
        public ActionResult Insertar(User request)
        {
            var registroCorrecto = _accountService.RegistrarUsuario(request);
            if (!registroCorrecto)
            {
                return BadRequest("Ocurrio un error en la solicitud envaida");
            }
            return Ok("Se registro correctamente el usuario");
        }

        [HttpPut]
        public ActionResult Actualizar(User usuario)
        {
            User usuarioExistente = null;
            try
            {
                // obtener el producto de BD
                usuarioExistente = _cibertecContext.Users.Find(usuario.Id);
            }
            catch (System.Exception)
            {
                return BadRequest("No se pudo obtener la información del usuario existente");
            }
            // si no existe, devolver un error
            if (usuarioExistente == null)
            {
                return BadRequest("El usuario no existe");
            }

            usuarioExistente.Email = usuario.Email == null ? usuarioExistente.Email : usuario.Email;
            usuarioExistente.Name = usuario.Name;
            usuarioExistente.Roles = usuario.Roles;
            usuarioExistente.Dni = usuario.Dni;
            usuarioExistente.Password = usuario.Password;

            try
            {
                var resultado = _cibertecContext.Commit();

                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                // log del error (ex)
                return BadRequest("Ocurrió un error al tratar de grabar en BD");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            // obtener la entidad que se quiere eliminar
            var Eliminar = _cibertecContext.Users.Find(id);
            if (Eliminar == null)
            {
                return BadRequest("El producto no existe");
            }

            _cibertecContext.Users.Remove(Eliminar);

            return Ok(_cibertecContext.Commit());
        }

    }
}
