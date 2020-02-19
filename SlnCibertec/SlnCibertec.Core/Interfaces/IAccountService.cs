using SlnCibertec.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Interfaces
{
    public interface IAccountService
    {
        User ValidateUser(string email, string password);
        // agregar otros métodos relacionados con el usuario
        bool RegistrarUsuario(User nuevoUsuario);
    }
}
