import React from "react";
import { IPageBaseProps } from "../types";
import * as SignalR from "@aspnet/signalr";

interface IMensaje {
    username: string;
    message: string;
}

export function Chat(props: IPageBaseProps) {
    // crear una variable del state para mantener la conexión con el hub
    const [hubConnection, setHubConnection] = React.useState<SignalR.HubConnection>();
    const [mensajes, setMensajes] = React.useState<IMensaje[]>([]);
    const [nuevoMensaje, setNuevoMensaje] = React.useState<IMensaje>();

    // configurar un efecto para cuando llega un nuevo mensaje
    React.useEffect(() => {
        // crear un arreglo temporal
        const tmpArray = [...mensajes];
        if (nuevoMensaje) {
            tmpArray.push(nuevoMensaje);
            setMensajes(tmpArray);
        }
    }, [nuevoMensaje])

    // cuando se carga la página por primera vez, inicializar la conexión
    React.useEffect(() => {
        // instanciar la conexión
        const newConnection = new SignalR.HubConnectionBuilder().withUrl(`${process.env.REACT_APP_BASE_URL}/chathub`).build();

        // configurar lo que pasa cuando se recibe una señal por parte del hub
        newConnection.on("mensajeRecibido", function (mensaje: string, nombreUsuario: string) {
            console.log(`Se ha recibido el mensaje ${mensaje} del usuario ${nombreUsuario}`)

            // setear el nuevo mensaje
            setNuevoMensaje({ username: nombreUsuario, message: mensaje });
        })

        // iniciar la conexión
        newConnection.start().catch(error => console.error(error));

        // guardar la conexión en el state
        setHubConnection(newConnection);
    }, []);

    const onHandleSubmit = (e: any) => {
        e.preventDefault();
        // obtener el input del usuario
        const mensaje = e.target["mensaje"].value;
        const nombreUsuario = e.target["nombreUsuario"].value;

        // enviar los valores al hub
        if (hubConnection) {
            hubConnection.send("enviarMensaje", mensaje, nombreUsuario).then(() => console.log("se envió el mensaje"))
        }
    }

    // método para mostrar los mensajes
    const mostrarMensajes = () => {
        const listaMensajesVista = mensajes.map((mensaje, index) => {
            return <li key={index}>
                <b>{mensaje.username}</b>: {mensaje.message}
            </li>
        })

        return listaMensajesVista;
    }

    return <div>
        <h2>Chat</h2>
        <form onSubmit={onHandleSubmit}>
            <input type="text" name="nombreUsuario"></input>
            <input type="text" name="mensaje"></input>
            <button>Enviar</button>
        </form>
        <div>
            <h2>Mensajes</h2>
            <ul id="lista-mensajes">
                {mostrarMensajes()}
            </ul>
        </div>
    </div>
}