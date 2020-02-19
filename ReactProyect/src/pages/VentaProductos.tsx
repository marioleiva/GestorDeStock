import React from "react";
import { IPageBaseProps, IProducto } from "../types";
import { Title } from "../components/global/Title";
import { insertarProduct } from "../services/productoService";

export default function InsertarProducto(props: IPageBaseProps) {
    const [mensaje, setMensaje] = React.useState<string>("");    
    const formSubmit = (e: any) => {
        e.preventDefault();
        // obtener los valores que el usuario ha ingresado en el form
        const newProduct: Partial<IProducto> = {}

        newProduct.nombre = e.target["productName"].value;
        newProduct.descripcion = e.target["Descripcion"].value;
        newProduct.cantidad = e.target["cantidad"].value;
        newProduct.precio = e.target["precio"].value;

        // llamar a la función local para guardar el producto
        guardarProducto(newProduct);
    }

    const guardarProducto = async (nuevoProducto: Partial<IProducto>) => {
        const resultadoServicio = await insertarProduct(nuevoProducto);
        setMensaje(resultadoServicio);        
    }
    return <div>
        <Title texto="Insertar un Producto"></Title>
        <form className="mt-3" onSubmit={formSubmit}>
            <div className="form-group">
                <label htmlFor="ventaId">Venta ID</label>
                <input className="form-control" type="text" name="ventaId" id="ventaId" placeholder=""></input>
            </div>
            <div className="form-group">
                <label htmlFor="productoId">Producto</label>
                <input className="form-control" type="text" name="productoId" id="productoId" placeholder=""></input>
            </div>
            <div className="form-group">
                <label htmlFor="cantidad">Cantidad</label>
                <input className="form-control" type="text" name="cantidad" id="cantidad" placeholder=""></input>
            </div>
            
            
           
            <button type="submit" className="btn btn-primary">Guardar</button>
        </form>

        <p className="mt-3">Resultado de Venta: {mensaje}</p>
    </div>
}