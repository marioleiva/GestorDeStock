import axios from "axios";

export interface ITokenResponse {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
}

export async function obtenerToken(username: string, password: string): Promise<ITokenResponse> {
    // crear el objeto que vamos a enviar
    const data = { username, password };
    // realizar el post a /account/token
    const response = await axios.post(`${process.env.REACT_APP_BASE_URL}/account/token`, data, {
        headers: {
            "Content-Type": "application/json"
        }
    });

    // obtener el token de la respuesta
    return response.data as ITokenResponse;
}

export async function refreshToken(): Promise<ITokenResponse> {
    // crear el objeto que vamos a enviar
    const data = { refreshToken: localStorage.getItem("cibertec-refresh-token") };
    // realizar el post a /account/token
    const response = await axios.post(`${process.env.REACT_APP_BASE_URL}/account/token/refresh`, data, {
        headers: {
            "Content-Type": "application/json"
        }
    });

    // obtener el token de la respuesta
    return response.data as ITokenResponse;
}