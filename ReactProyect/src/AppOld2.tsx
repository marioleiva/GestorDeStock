import React, { useState } from "react";
import { ListaCategorias } from "./pages/ListaCategorias";
import { ListaProductos } from "./pages/ListaProductos";

export default function App() {
    const [vistaAcutal, setVistaActual] = useState<string>("productos");

    function devolverVistaActual(){
        switch(vistaAcutal){
            case "productos": return <ListaProductos></ListaProductos>
            case "categorias": return <ListaCategorias></ListaCategorias>
            default: return null;
        }        
    }
    return <div>
        <ul>
            <li><a className="btn btn-link" href="#" onClick={()=>{setVistaActual("productos")}}>Lista de Productos</a></li>
            <li><a className="btn btn-link" href="#" onClick={()=>{setVistaActual("categorias")}}>Lista de Categorías</a></li>
        </ul>
        {devolverVistaActual()}
    </div>
}