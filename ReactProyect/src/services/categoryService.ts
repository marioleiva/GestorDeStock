import axios from "axios";
import { ICategory } from "../types";

export async function obtenerTodos(): Promise<ICategory[]> {
    // obtener el resultado de la solicitud en una variable
    const resultado = await axios.get(`${process.env.REACT_APP_BASE_URL}/categories`, {
        headers: {
            Authorization: `Bearer ${localStorage.getItem("cibertec-token")}`
        }
    });

    // devovler la data obtenida del resultado, casteandola como un arreglo de ICategory
    return resultado.data as ICategory[];
}