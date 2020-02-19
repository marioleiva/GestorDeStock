using SlnCibertec.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Interfaces
{
    public interface IClienteService
    {
        bool RegistrarCliente(Cliente nuevoCliente);
    }
}
