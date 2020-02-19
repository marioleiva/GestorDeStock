import React from "react";
import { IProducto } from "../../types";
import { Link } from "@reach/router";

interface IProps {
    producto: IProducto;
    onDeleteProduct: (productoId: number) => Promise<void>;
}

export function TablaProductosFila(props: IProps) {
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
            <Link to={`/productos/${props.producto.productoId}`}>Detalle</Link>
            <Link to={`/productos/Update/${props.producto.productoId}`}>Editar</Link>
            <button className="btn btn-link btn-sm" onClick={() => { props.onDeleteProduct(props.producto.productoId) }}>Borrar</button>
        </td>
    </tr>
}