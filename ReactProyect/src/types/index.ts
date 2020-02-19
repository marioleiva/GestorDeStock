// definir las interfaces generales del proyecto
export interface IProducto {
    productoId: number;
    nombre: string;
    descripcion: string;
    cantidad: number;
    precio: number;
}
/*export interface IProduct {
    productId: number;
    productName: string;
    unitPrice: number;
}
*///

export interface ICategory {
    categoryId: number;
    categoryName: string;
    description: string;
    picture: string;
}

export interface ICliente {
    Id: number;
    Nombre: string;
    Apellido: string;
    NroDocumento: string;
    Telefono: string;
}

export interface IProveedor {
    Id: number;
    Empresa: string;
    NroDocumento: string;
    Representante: string;
    Celular: string;
    Correo: string;
}

export interface IVenta {
    Id: number;
    Total: number;
    ClienteId: number;
    UserId: number;
}
export interface IDetalleVenta {
    id: number;
    ventaId: number;
    cantidad: number;
    productoId: number;
    subTotal: number;
    
}

/**
 * Esta interface servira para definir las props base de cada página de nuestro app
 */
export interface IPageBaseProps {
    path?: string;
}