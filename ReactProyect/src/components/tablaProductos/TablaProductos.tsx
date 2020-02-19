import React from "react";
import { IProducto } from "../../types";
import { TablaProductosHeader } from "./TablaProductosHeader";
import { TablaProductosFila } from "./TablaProductosFila";

interface IProps {
    data: IProducto[] // un arreglo de IProduct
    onDeleteProducts: (ProductoId: number) => Promise<void>;
}

export function TablaProductos(props: IProps) {
    return <table className="table table-bordered">
        <TablaProductosHeader></TablaProductosHeader>
        <tbody>
            {props.data.map((productoIterador) => {
                return <TablaProductosFila key={productoIterador.productoId}
                    producto={productoIterador} onDeleteProduct={props.onDeleteProducts}></TablaProductosFila>
            })}
        </tbody>
    </table>
}