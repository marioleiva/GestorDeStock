import React, { useState, useEffect } from "react";
import { IProducto, IVenta, IDetalleVenta, IPageBaseProps } from "../types";
import { Title } from "../components/global/Title";
import { DescripcionCantidad } from "../components/global/DescripcionCantidad";
import { TablaProductosVenta } from "../components/tablaProductosVenta/TablaProductosVenta";
import { obtenerTodos, deleteProduct } from "../services/productoService";
import { insertarVenta, EditarVenta } from "../services/VentaService";
import loading from './loading.svg';
import { Link } from "@reach/router";

export function ElegirProducto(props: IPageBaseProps) {

    // utilizar el state para manjear una variable data
    const [data, setData] = useState<IProducto[]>([]);
    const [cargando, setCargando] = useState<boolean>(true);

    async function cargarProductos() {
        var productos = await obtenerTodos();
        setData(productos);
        AgregarVenta(4);
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


    const AgregarVenta = async (productId: number) => {
        
        const nuevaVenta: Partial<IVenta> = {}
        nuevaVenta.ClienteId = 1;
        nuevaVenta.UserId = 1;
        guardarVenta(nuevaVenta);
    }
    const guardarVenta = async (nuevaVenta: Partial<IVenta>) => {
        const idVenta = await insertarVenta(nuevaVenta); 
        localStorage.setItem("idVenta", idVenta);       
    }

     const AgregarProducto = async (productId: number) => {
        const newDetalleVenta: Partial<IDetalleVenta> = {}
        newDetalleVenta.cantidad = 0;
        newDetalleVenta.productoId = productId;
        newDetalleVenta.ventaId = Number(localStorage.getItem("idVenta")); //3;
        guardarDetalleVenta(newDetalleVenta);
    }
    const guardarDetalleVenta = async (newDetalleVenta: Partial<IDetalleVenta>) => {
            const resultadoServicio = await EditarVenta(newDetalleVenta);     
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
        {/* <Link to="/Venta/Detalle" className="btn btn-primary btn-sm mb-3">Ver carrito</Link> */}
        <TablaProductosVenta data={data} onDeleteProducts={AgregarProducto}></TablaProductosVenta>
        <Link to={`/Venta/Detalle/${Number(localStorage.getItem("idVenta"))}`}>Editar</Link>
    </div>
}