import React from "react";
import { IProducto } from "../../types";
import { TablaProductosHeaderVenta } from "./TablaProductosHeaderVenta";
import { TablaProductosFilaVenta } from "./TablaProductosFilaVenta";

interface IProps {
    data: IProducto[] // un arreglo de IProduct
    onDeleteProducts: (ProductoId: number) => Promise<void>;
}

export function TablaProductosVenta(props: IProps) {
    return <table className="table table-bordered">
        <TablaProductosHeaderVenta></TablaProductosHeaderVenta>
        <tbody>
            {props.data.map((productoIterador) => {
                return <TablaProductosFilaVenta key={productoIterador.productoId}
                    producto={productoIterador} onDeleteProduct={props.onDeleteProducts}></TablaProductosFilaVenta>
            })}
        </tbody>
    </table>
}