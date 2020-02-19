import React from "react";
import { IDetalleVenta } from "../../types";
import { Link } from "@reach/router";

interface IProps {
    detalle: IDetalleVenta;
    onDeleteProduct: (id: number) => Promise<void>;
}

export function TablaVentaDetalleFila(props: IProps) {
    return <tr key={props.detalle.id}>
        <td>
            {props.detalle.productoId}
        </td>
        <td>
            {props.detalle.cantidad}
        </td>
        <td>
            {props.detalle.subTotal}
        </td>
        <td>          
            <button className="btn btn-link btn-sm" onClick={() => { props.onDeleteProduct(props.detalle.id) }}>Borrar</button>
        </td>
    </tr>
}