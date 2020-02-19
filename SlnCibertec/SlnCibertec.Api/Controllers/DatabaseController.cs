using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SlnCibertec.Core.Interfaces;
using SlnCibertec.Infra.Data;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SlnCibertec.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ICibertecContext _cibertecContext;
        public DatabaseController(ICibertecContext cibertecContext)
        {
            _cibertecContext = cibertecContext;
        }

        [HttpGet("migrar")]
        [HttpGet("migrar2")]
        [HttpGet("migrar3")]
        [HttpGet("migrar123")]
        public string Migrar()
        {
            // migrar la BD utilizando el contexto
            _cibertecContext.Migrate();
            return "Migración exitosa";
        }        
    }
}
