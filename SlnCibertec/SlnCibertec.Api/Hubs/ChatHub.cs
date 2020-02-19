using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SlnCibertec.Api.Hubs
{
    // le decimos que herede de la clase de SignalR llamada Hub
    public class ChatHub : Hub
    {
        public async Task EnviarMensaje(string mensaje, string nombreUsuario)
        {
            // cada vez que un usuario invoque este evento, el hub lo re transmitirá a todos los clientes conectados
            await Clients.All.SendAsync("mensajeRecibido", mensaje, nombreUsuario);
        }
    }
}
