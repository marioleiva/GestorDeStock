using Microsoft.AspNetCore.SignalR;
using SlnCibertec.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlnCibertec.Api.Hubs
{
    public class ProductHub: Hub
    {
        public async Task ModificarProducto(Product product)
        {
            await Clients.All.SendAsync("actualizarLista", product);
        }

        public async Task ActualizarPos(int latitude, int longitude)
        {
            await Clients.All.SendAsync("actualizarPosCarrito", latitude, longitude);
        }
    }
}
