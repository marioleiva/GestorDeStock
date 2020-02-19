import React from "react";

interface IProps {
    texto: string;
}

export function Title(props: IProps) {
    const textoTitulo = !props.texto ? "Título no definido" : props.texto;
    return <h1>{textoTitulo}</h1>
}