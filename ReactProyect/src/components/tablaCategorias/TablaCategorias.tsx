import React from "react";
import { ICategory } from "../../types";
import { TablaCategoriasHeader } from "./TablaCategoriasHeader";
import { TablaCategoriasFila } from "./TablaCategoriasFila";

interface IProps {
    data: ICategory[]
}

export function TablaCategorias(props: IProps) {
    return <table className="table table-bordered">
        <TablaCategoriasHeader></TablaCategoriasHeader>
        <tbody>
            {props.data.map((categoriaIterador) => {
                return <TablaCategoriasFila key={categoriaIterador.categoryId}
                    categoria={categoriaIterador}></TablaCategoriasFila>
            })}
        </tbody>
    </table>
}