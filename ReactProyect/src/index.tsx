import React from 'react';
import ReactDOM from 'react-dom';
// importar toda la librería de bootstrap
import "bootstrap/dist/css/bootstrap.min.css";
import App from './App';
import { ListaProductos } from './pages/ListaProductos';
import { Contador } from './components/Ejercicios';
import { ListaCategorias } from './pages/ListaCategorias';

// ReactDOM.render(<Contador />, document.getElementById('root'));
// ReactDOM.render(<ListaProductos />, document.getElementById('root'));
// ReactDOM.render(<ListaCategorias />, document.getElementById('root'));
ReactDOM.render(<App />, document.getElementById('root'));
// ReactDOM.render(<h1>Hola mundo</h1>, document.getElementById('root'));

const titulo = <h1>Hola mundo</h1>;

const tabla = <table>
    <thead>
        <tr>
            <th>
                Columna1
            </th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Valor 1</td>
        </tr>
        <tr>
            <td>Valor 2</td>
        </tr>
    </tbody>
</table>

// ReactDOM.render(tabla, document.getElementById('root'));