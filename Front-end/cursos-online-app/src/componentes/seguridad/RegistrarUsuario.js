import { Button, Container, Grid, TextField, Typography } from '@material-ui/core';
import React, { useState } from 'react';
import style from '../Tool/Style'



const RegistrarUsuario = () => {

    const [usuario, setUsuario]  = useState({
        NombreCompleto: "",
        Email : "",
        Password: "",
        ConfirmarPassword: "",
        UserName : ""
    });


    const ingresarValoresMemoria = e =>{
        const {name, value} = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }));
    };


    const registrarUsuario = e =>{
        e.preventDefault();
        console.log("imprime los valores de memoria temporal del usuario", usuario);
    }

    return (

        <Container component = "main" maxWidth="md" justify= "center">
                <div style ={style.paper} >
                    <Typography component= "h1" variant = "h5">

                        Registro de Usuario
                    </Typography>

                    <form style = {style.form}>

                        <Grid container spacing ={2}>
                            <Grid item xs ={12} md = {12}>
                                <TextField name = "NombreCompleto" value = {usuario.NombreCompleto} onChange = {ingresarValoresMemoria} variant = "outlined" fullWidth label = "Ingrese nombre y apellidos">

                                </TextField>
                            </Grid>

                            

                            <Grid item xs ={12} md ={6}>
                                <TextField name = "Email" value ={usuario.Email} onChange = {ingresarValoresMemoria} type = "email" variant = "outlined" fullWidth label = "Ingrese su email">

                                </TextField>

                            </Grid>


                            <Grid item xs ={12} md ={6}>
                                <TextField name = "UserName" value = {usuario.UserName} onChange = {ingresarValoresMemoria} variant = "outlined" fullWidth label = "Ingrese su username">

                                </TextField>
                            </Grid>

                            <Grid item xs ={12} md ={6}>
                                <TextField name = "Password" value = {usuario.Password} onChange = {ingresarValoresMemoria} type = "password" variant = "outlined" fullWidth label = "Ingrese su password">

                                </TextField>
                            </Grid>

                            <Grid item xs ={12} md ={6}>
                                <TextField name = "ConfirmarPassword" value = {usuario.ConfirmarPassword} onChange = {ingresarValoresMemoria} type = "password" variant = "outlined" fullWidth label = "Confirme el password">

                                </TextField>
                            </Grid>

                        </Grid>

                        <Grid container justify="center">
                            <Grid item xs ={12} md = {6}>
                                <Button type = "submit" onClick ={registrarUsuario} fullWidth variant="contained" color = "primary" size = "large" style ={style.submit} >
                                    Enviar
                                </Button>

                            </Grid>

                        </Grid>

                    </form>
                </div>

        </Container>
    );
}


export default RegistrarUsuario;