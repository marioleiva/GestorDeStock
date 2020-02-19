import React from "react";
import { ICategory } from "../../types";

interface IProps {
    categoria: ICategory;
}

export function TablaCategoriasFila(props: IProps) {
    return <tr>
        <td>
            {props.categoria.categoryName}
        </td>
        <td>
            {props.categoria.description}
        </td>
    </tr>
}