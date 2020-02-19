import React, { useState, useEffect } from "react";

export default function Principal() {
    return <h1>Principal</h1>
}

export function Secundario() {
    return <h2>Elemento Secundario</h2>
}

interface IMostrarNombreProps {
    nombre: string;
    numeroDeVeces: number;
}

interface IMostrarNombresProps {
    nombres: string[];
}

export function MostrarNombres(props: IMostrarNombresProps) {
    // validar que el arreglo enviado no está vacío
    if (!props.nombres || props.nombres.length === 0) {
        return <b>No se han enviado nombres para mostrar!</b>
    }
    return <>
        {props.nombres.map((nombre) => {
            return <p>Tu Nombre es: <b>{nombre}</b></p>
        })}
    </>
}

/**
 * Este componente pinta el nombre tantas veces se haya indicado
 * @param props 
 */
export function MostrarNombre(props: IMostrarNombreProps) {
    const arregloDeNombres = [];

    for (let index = 0; index < props.numeroDeVeces; index++) {
        arregloDeNombres.push(<p>Tu Nombre es: <b>{props.nombre}</b></p>)
    }

    return <>
        {arregloDeNombres}
    </>
}

interface IBotonCibertecProps {
    texto: string;
    color?: string; // el color es opcional
    // mensajeClick?: string;
    onBtnClick: () => void;
}

// function BotonCibertec({ texto, color }: { texto: string; color: string }) { }
// function BotonCibertec({ texto, color }: IBotonCibertecProps) { }
export function BotonCibertec(props: IBotonCibertecProps) {
    // obtener el color
    const btnBackgroundColor = !props.color ? "#fff" : props.color;
    // obtener el mensaje para mostrar al hacer clic
    // const clickMessage = !props.mensajeClick ? "Mensaje" : props.mensajeClick;
    return <button style={{
        backgroundColor: btnBackgroundColor, padding: 10, border: "1px solid #000", marginLeft: 10
    }} onClick={props.onBtnClick}>
        {props.texto}
    </button>
}

export function Contador() {    
    console.log("Se esta llamando a la funcion contador");
    // el hook useState devuelve dos variables constantes:
    // 1. El valor de la variable del estado
    // 2. Una función para cambiar el valor de esa variable en el estado
    const [valor, setValor] = useState<number>(0);

    // el hook useEffect sirve para ejecutar código cuando se realicen cambios específicos de las variables del componente (incluyendo el estado) o también para controlar la primera renderización del componente

    useEffect(()=>{
        setValor(100);
    },[]) // el [] significa que este código se ejecutará solo la primera vez

    useEffect(()=>{
        setValor(100);
    },[valor])
    
    return <div>
        <span>Contador: {valor}</span>

        <button onClick={() => { setValor(valor + 1); }}>Incrementar</button>
        <button onClick={() => { setValor(valor - 1); }}>Disminuir</button>
    </div>
}