import axios from "axios";
import { IProducto, IVenta, IDetalleVenta } from "../types";
import { refreshToken } from "./authService";
import { obtenerAuthToken } from "./baseService";

// url base de los servicios web (proyecto VS 2017)
const BASE_URL = process.env.REACT_APP_BASE_URL;

export async function obtenerPorId(ventaId: number): Promise<IDetalleVenta[]> {
    const resultado = await axios.get(`${BASE_URL}/DetalleVenta/${ventaId}`, {
        headers: {
            Authorization: `Bearer ${await obtenerAuthToken()}`
        }
    })
    return resultado.data as IDetalleVenta[];
}

export async function deleteProduct(productId: number): Promise<number> {
    const resultado = await axios.delete(`${BASE_URL}/DetalleVenta/${productId}`, {
        headers: {
            Authorization: `Bearer ${await obtenerAuthToken()}`
        }
    });
    return resultado.data;
}
