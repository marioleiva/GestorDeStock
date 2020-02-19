import React, { useState, useEffect } from "react";
import { IProducto, IPageBaseProps } from "../types";
import { Title } from "../components/global/Title";
import { DescripcionCantidad } from "../components/global/DescripcionCantidad";
import { TablaProductos } from "../components/tablaProductos/TablaProductos";
import { obtenerTodos, deleteProduct } from "../services/productoService";
import loading from './loading.svg';
import { Link } from "@reach/router";

export function ListaProductos(props: IPageBaseProps) {

    // utilizar el state para manjear una variable data
    const [data, setData] = useState<IProducto[]>([]);
    const [cargando, setCargando] = useState<boolean>(true);

    async function cargarProductos() {
        var productos = await obtenerTodos();
        setData(productos);

        // una vez que tengamos respuesta del servicio, ocultar el indicador de cargando
        setCargando(false);
    }

    // definir un efecto para cuando el componente cargue por primera vez
    useEffect(() => {
        // simular un servicio lento
        setTimeout(() => {
            cargarProductos();
        }, 500);

    }, [])

    const borrarProducto = async (productId: number) => {
        const resultadoBorrar = await deleteProduct(productId);
        if (resultadoBorrar > 0) {
            cargarProductos();
        }
    }

    const descripcionCantidad = `Hay ${data.length} de productos registrados`;

    // mostrar el indicador de cargando
    if (cargando) {
        return <div className="d-flex justify-content-center">
            <img src={loading}></img>
        </div>

    }

    return <div>
        <Title texto="Lista de Productos"></Title>
        <DescripcionCantidad texto={descripcionCantidad}></DescripcionCantidad>
        <Link to="/productos/insert" className="btn btn-primary btn-sm mb-3">Crear nuevo producto</Link>
        <TablaProductos data={data} onDeleteProducts={borrarProducto}></TablaProductos>
    </div>
}