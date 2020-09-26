import { Avatar, Button, Container, TextField, Typography } from '@material-ui/core';
import React, { useState } from 'react'
import style from '../Tool/Style';
import HttpsOutlinedIcon from '@material-ui/icons/HttpsOutlined';
import { loginUsuario } from '../../actions/UsuarioActions';


function Login() {


    const [usuario, setUsuario] = useState({
        Email: '',
        Password : ''
    });





    const ingresarValoresMemoria = e =>{
        const {name, value} = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }));
    };


    const loginUsuariBoton = e =>{
        e.preventDefault();
        loginUsuario(usuario).then(response =>{
            console.log("Login exitoso", response);
            window.localStorage.setItem("token_seguridad", response.data.token)
        });
    }


    return (
        <Container maxWidth = "xs">
            <div style={style.paper} >

                <Avatar style={style.avatar} >
                    <HttpsOutlinedIcon style = {style.icon}/>
                </Avatar>

                <Typography component = "h1" variant = "h5">
                    Login de Usuario
                </Typography>

                <form style= {style.form}>
                    <TextField name = "Email" value = {usuario.Email} onChange = {ingresarValoresMemoria} variant = "outlined" label = "Ingrese userName"  fullWidth margin ="normal"/>

                    <TextField name = "Password" value = {usuario.Password} onChange = {ingresarValoresMemoria} variant = "outlined" type ="password" label = "Ingrese su password" fullWidth margin ="normal" />
               
               
                    <Button type = "submit" onClick={loginUsuariBoton} fullWidth variant =  "contained" color = "primary"  style = {style.submit}>
                        Enviar
                    </Button>
               
                </form>

                

            </div>
        </Container>
    )
}


export default Login

