import React from 'react';
import './App.css';
import Principal, { Secundario, MostrarNombres, MostrarNombre, BotonCibertec } from "./components/Ejercicios";

function procesoQueTomaMuchoTiempo() {
  setTimeout(function () {
    alert("Terminó el proceso")
  }, 3000)
}

const App: React.FC = () => {
  // const paramestrosNombre = {
  //   nombre: "Arturo 1",
  //   edad: 123
  // }
  return (
    <>
      <div className="App">
        Cibertec React
        <Principal />
        <Secundario />
        <MostrarNombres nombres={["nombre 1", "nombre 2", "nombre 3"]} />
        <MostrarNombres nombres={[]} />
        <MostrarNombre nombre="Arturo" numeroDeVeces={100} />
        <MostrarNombre nombre="John" numeroDeVeces={2} />
        {/* <MostrarNombre {...paramestrosNombre} /> */}
        <BotonCibertec
          texto="Botón por default"
          onBtnClick={procesoQueTomaMuchoTiempo}></BotonCibertec>
        <BotonCibertec
          texto="Botón por default 2"
          onBtnClick={() => { console.log("mensaje 1") }}></BotonCibertec>
        <BotonCibertec
          texto="Botón por default 3"
          color="#000"
          onBtnClick={() => { alert("mensaje 123") }}></BotonCibertec>
      </div>
      <div>
        asdasd
      </div>
    </>
  );
}

export default App;
