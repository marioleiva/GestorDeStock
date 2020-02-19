import React from "react";
import { IDetalleVenta } from "../../types";
import { TablaVentaDetalleHeader } from "./TablaVentaDetalleHeader";
import { TablaVentaDetalleFila } from "./TablaVentaDetalleFila";

interface IProps {
    data: IDetalleVenta[] // un arreglo de IProduct
    onDeleteProducts: (id: number) => Promise<void>;
}

export function TablaVentaDetalle(props: IProps) {
    return <table className="table table-bordered">
        <TablaVentaDetalleHeader></TablaVentaDetalleHeader>
        <tbody>
            {props.data.map((productoIterador) => {
                return <TablaVentaDetalleFila key={productoIterador.id}
                    detalle={productoIterador} onDeleteProduct={props.onDeleteProducts}></TablaVentaDetalleFila>
            })}
        </tbody>
    </table>
}