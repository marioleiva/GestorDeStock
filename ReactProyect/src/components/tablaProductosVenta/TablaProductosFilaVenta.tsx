import React from "react";
import { IProducto } from "../../types";
import { Link } from "@reach/router";

interface IProps {
    producto: IProducto;
    onDeleteProduct: (productoId: number) => Promise<void>;
}

export function TablaProductosFilaVenta(props: IProps) {
    return <tr key={props.producto.productoId}>
        <td>
            {props.producto.nombre}
        </td>
        <td>
            {props.producto.cantidad}
        </td>
        <td>
            {props.producto.precio}
        </td>
        <td>
            <button className="btn btn-link btn-sm" onClick={() => { props.onDeleteProduct(props.producto.productoId) }}>Elegir</button>
            
        </td>
    </tr>
}