using SlnCibertec.Core.Entities;
using SlnCibertec.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlnCibertec.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICibertecContext _context;
        public AccountService(ICibertecContext context)
        {
            _context = context;
        }

        public bool RegistrarUsuario(User nuevoUsuario)
        {
            // validaciones
            if (string.IsNullOrEmpty(nuevoUsuario.Email) || (string.IsNullOrEmpty(nuevoUsuario.Name)) || (string.IsNullOrEmpty(nuevoUsuario.Dni)) || (string.IsNullOrEmpty(nuevoUsuario.Password)))
            {
                return false;
            }

            var ClienteConDNIRepetido = _context.Users.Where(p => p.Email.ToUpper() == nuevoUsuario.Email.ToUpper());
            if (ClienteConDNIRepetido.Count() > 0)
            {
                return false;
            }
            // agregar el proucto a BD
            _context.Users.Add(nuevoUsuario);

            return _context.Commit() > 0;
        }

        public User ValidateUser(string email, string password)
        {
            // obtener el registro de la bd
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            return user;
        }
    }
}
