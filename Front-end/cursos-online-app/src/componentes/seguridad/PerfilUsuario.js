import { Button, Container, Grid, TextField, Typography } from '@material-ui/core';
import React, { useState, useEffect } from 'react'
import { actualizarUsuario, obtenerUsuarioActual } from '../../actions/UsuarioActions';
import style from '../Tool/Style';



const PerfilUsuario = () =>  {



const [usuario, setUsuario] = useState({
    nombreCompleto : '',
    email : '',
    password : '',
    confirmarPassword : '',
    userName : ''
});


const ingresarValoresMemoria = e =>{

    const {name, value} = e.target;
    setUsuario(anterior => ({
        ...anterior,
        [name] : value
    }));
};



useEffect(() => {

    obtenerUsuarioActual().then(response => {
        console.log("esta es la data del response", response);
        setUsuario(response.data);
    } );
}, []);



const guardarUsuario = e => {
    e.preventDefault();

    actualizarUsuario(usuario).then(response =>{
        window.localStorage.setItem("token_seguridad", response.data.token);
    });
};


        return (
            
            <Container component = "main" maxWidth = "md" justify = "center">
                <div style = {style.paper}>
                    <Typography component = "h1" variant = "h5">
                        Perfil de usuario
                    </Typography>

                </div>

                <form style = {style.form} >
                    <Grid container spacing= {2}>
                        <Grid item xs = {12} md = {12}>
                            <TextField name = "nombreCompleto" value = {usuario.nombreCompleto} onChange = {ingresarValoresMemoria} variant = "outlined" fullWidth label = "Ingrese nombre y apellidos"></TextField>                    
                        </Grid>

                        <Grid item xs = {12} md = {6}>
                            <TextField name = "userName" value = {usuario.userName}  onChange = {ingresarValoresMemoria} variant = "outlined" fullWidth label = "Ingrese su UserName"></TextField>                    
                        </Grid>

                        <Grid item xs = {12} md = {6}>
                            <TextField name = "email" value = {usuario.email} type = "email" onChange = {ingresarValoresMemoria} variant = "outlined" fullWidth label = "Ingrese su email"></TextField>                    
                        </Grid>

                        <Grid item xs = {12} md = {6}>
                            <TextField name = "password" value = {usuario.password} type = "password"  onChange = {ingresarValoresMemoria} variant = "outlined" fullWidth label = "Ingrese su password"></TextField>                    
                        </Grid>

                        <Grid item xs = {12} md = {6}>
                            <TextField name = "confirmarPassword" value = {usuario.confirmarPassword} type = "password" onChange = {ingresarValoresMemoria} variant = "outlined" fullWidth label = "Confirmar su password"></TextField>                    
                        </Grid>

                    </Grid>

                    <Grid container justify = "center">
                        <Grid item xs ={12} md = {6}>
                            <Button type = "submit" onClick= {guardarUsuario} fullWidth variant = "contained" color = "primary" size = "large" style = {style.submit}>
                                Guardar datos
                            </Button>

                        </Grid>

                    </Grid>

                </form>
            </Container>
        );
    };



export default PerfilUsuario;

