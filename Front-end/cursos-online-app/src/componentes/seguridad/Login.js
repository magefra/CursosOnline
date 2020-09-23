import { Avatar, Button, Container, TextField, Typography } from '@material-ui/core';
import React from 'react'
import style from '../Tool/Style';
import HttpsOutlinedIcon from '@material-ui/icons/HttpsOutlined';



function Login() {
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
                    <TextField variant = "outlined" label = "Ingrese userName" name = "username" fullWidth margin ="normal"/>

                    <TextField variant = "outlined" type ="password" label = "Ingrese su password" name = "password" fullWidth margin ="normal" />
               
               
                    <Button type = "submit" fullWidth variant =  "contained" color = "primary"  style = {style.submit}>
                        Enviar
                    </Button>
               
                </form>

                

            </div>
        </Container>
    )
}


export default Login

