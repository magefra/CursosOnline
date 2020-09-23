import { Button, Container, Grid, TextField, Typography } from '@material-ui/core';
import React from 'react'
import style from '../Tool/Style';



const PerfilUsuario = () =>  {
        return (
            
            <Container component = "main" maxWidth = "md" justify = "center">
                <div style = {style.paper}>
                    <Typography component = "h1" variant = "h5">
                        Perfil de usuario
                    </Typography>

                </div>

                <form style = {style.form} >
                    <Grid container spacing= {2}>
                        <Grid item xs = {12} md = {6}>
                            <TextField name = "nombrecompleto" variant = "outlined" fullWidth label = "Ingrese nombre y apellidos"></TextField>                    
                        </Grid>


                        <Grid item xs = {12} md = {6}>
                            <TextField name = "email" type = "email" variant = "outlined" fullWidth label = "Ingrese su email"></TextField>                    
                        </Grid>

                        <Grid item xs = {12} md = {6}>
                            <TextField name = "password"  type = "password"  variant = "outlined" fullWidth label = "Ingrese su password"></TextField>                    
                        </Grid>

                        <Grid item xs = {12} md = {6}>
                            <TextField name = "confirmarpassword" type = "password" variant = "outlined" fullWidth label = "Confirmar su password"></TextField>                    
                        </Grid>

                    </Grid>

                    <Grid container justify = "center">
                        <Grid item xs ={12} md = {6}>
                            <Button type = "submit" fullWidth variant = "contained" color = "primary" size = "large" style = {style.submit}>
                                Guardar datos
                            </Button>

                        </Grid>

                    </Grid>

                </form>
            </Container>
        );
    };



export default PerfilUsuario;

