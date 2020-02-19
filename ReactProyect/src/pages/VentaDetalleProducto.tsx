import React, { useState, useEffect } from "react";
import { IProducto, IPageBaseProps, IDetalleVenta } from "../types";
import { Title } from "../components/global/Title";
import { DescripcionCantidad } from "../components/global/DescripcionCantidad";
import { TablaVentaDetalle } from "../components/tablaVentaDetalle/TablaVentaDetalle";
import { obtenerPorId, deleteProduct } from "../services/DetalleVentaService";
import loading from './loading.svg';
import { Link } from "@reach/router";

export function VentaDetalleProducto(props: IPageBaseProps) {

    // utilizar el state para manjear una variable data
    const [data, setData] = useState<IDetalleVenta[]>([]);
    const [cargando, setCargando] = useState<boolean>(true);

    async function cargarProductos2() {
        var detalleVenta = await obtenerPorId(6);
        setData(detalleVenta);

        // una vez que tengamos respuesta del servicio, ocultar el indicador de cargando
        setCargando(false);
    }

    // definir un efecto para cuando el componente cargue por primera vez
    useEffect(() => {
        // simular un servicio lento
        setTimeout(() => {
            cargarProductos2();
        }, 500);

    }, [])

    const borrarProducto = async (productId: number) => {
        const resultadoBorrar = await deleteProduct(productId);
        if (resultadoBorrar > 0) {
            cargarProductos2();
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
        <Title texto="Lista de Productos en boleta"></Title>
        <DescripcionCantidad texto={descripcionCantidad}></DescripcionCantidad>
        <TablaVentaDetalle data={data} onDeleteProducts={borrarProducto}></TablaVentaDetalle>
    </div>
}