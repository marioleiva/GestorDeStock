import React from "react";
import { IPageBaseProps, IProducto } from "../types";
import { obtenerPorId } from "../services/productoService";
import { Title } from "../components/global/Title";
import { EditarProduct } from "../services/productoService";

interface IProps extends IPageBaseProps {
    productId?: number;
}

export default function DetalleProducto(props: IProps) {
    // declarar una variable del estado para guardar la info del producto
    const [producto, setProduct] = React.useState<IProducto>();
    const [mensaje, setMensaje] = React.useState<string>("");  
    const formSubmit = (e: any) => {
        e.preventDefault();
        // obtener los valores que el usuario ha ingresado en el form
        const newProduct: Partial<IProducto> = {}
        newProduct.productoId = e.target["productId"].value;
        newProduct.nombre = e.target["productName2"].value;
        newProduct.descripcion = e.target["Descripcion2"].value;
        newProduct.cantidad = e.target["cantidad2"].value;
        newProduct.precio = e.target["precio2"].value;

        // llamar a la función local para guardar el producto
        guardarProducto(newProduct);
    }
    const guardarProducto = async (nuevoProducto: Partial<IProducto>) => {
        const resultadoServicio = await EditarProduct(nuevoProducto);
        setMensaje(resultadoServicio);        
    }

    React.useEffect(() => {
        async function getProduct(productId: number) {
            const resultado = await obtenerPorId(productId);
            setProduct(resultado);
        }

        if (props.productId) {
            getProduct(props.productId);
        }
    }, [])

    return <div>
<Title texto="Insertar un Producto"></Title>
<form className="mt-3" onSubmit={formSubmit}>

<div className="form-group">
        <label htmlFor="productId">Producto ID</label>
        <input className="form-control" readOnly type="text" value={props.productId} name="productId" id="productId" placeholder="Ingresa el nombre del producto"></input>
    </div>
    <div className="form-group">
        <label htmlFor="productName">Nombre</label>
        <input className="form-control" type="text" value={JSON.stringify(producto?.nombre)} name="productName2" id="productName2" placeholder="Ingresa el nombre del producto"></input>
    </div>
    <div className="form-group">
        <label htmlFor="Descripcion">Descripción</label>
        <input className="form-control" type="text" value={JSON.stringify(producto?.descripcion)} name="Descripcion2" id="Descripcion2" placeholder=""></input>
    </div>
    <div className="form-group">
        <label htmlFor="cantidad">Cantidad</label>
        <input className="form-control" type="Number"  name="cantidad2" id="cantidad2" placeholder=""></input>
    </div>
    <div className="form-group">
        <label htmlFor="precio">Precio</label>
        <input className="form-control" type="Number"  name="precio2" id="precio2" placeholder=""></input>
    </div>
    <button type="submit" className="btn btn-primary">Guardar</button>
</form>

<p className="mt-3">Resultado de insertar: {mensaje}</p>
</div>

}
