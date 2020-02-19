import React, { useState, useEffect } from "react";
import { IProducto, ICategory, IPageBaseProps } from "../types";
import { Title } from "../components/global/Title";
import { DescripcionCantidad } from "../components/global/DescripcionCantidad";
import { TablaCategorias } from "../components/tablaCategorias/TablaCategorias";
import { obtenerTodos } from "../services/categoryService";
import loading from './loading.svg';

export function ListaCategorias(props: IPageBaseProps) {
    // utilizar el state para manjear una variable data
    const [data, setData] = useState<ICategory[]>([]);
    const [cargando, setCargando] = useState<boolean>(true);

    // definir un efecto para cuando el componente cargue por primera vez
    useEffect(() => {
        async function cargarCategorias() {
            var categorias = await obtenerTodos();
            setData(categorias);

            // una vez que tengamos respuesta del servicio, ocultar el indicador de cargando
            setCargando(false);
        }

        // simular un servicio lento
        setTimeout(() => {
            cargarCategorias();
        }, 500);

    }, [])

    const descripcionCantidad = `Hay ${data.length} de categorías registrados`;

    // mostrar el indicador de cargando
    if (cargando) {
        return <div className="d-flex justify-content-center">
            <img src={loading}></img>
        </div>
    }

    return <div>
        <Title texto="Lista de Categorías"></Title>
        <DescripcionCantidad texto={descripcionCantidad}></DescripcionCantidad>
        <TablaCategorias data={data}></TablaCategorias>
    </div>
}