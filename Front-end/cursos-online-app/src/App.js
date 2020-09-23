import React from 'react';
import {ThemeProvider as MuithemeProvider} from '@material-ui/core/styles';
import theme from './theme/theme';
import RegistrarUsuario from './componentes/seguridad/RegistrarUsuario';
import Login from './componentes/seguridad/Login';
import PerfilUsuario from './componentes/seguridad/PerfilUsuario';


function App() {
  return (
        <MuithemeProvider theme= {theme}>

          <PerfilUsuario />

        </MuithemeProvider>
   
  );
}

export default App;