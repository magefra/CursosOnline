
import { Avatar, Button, IconButton, makeStyles, Toolbar, Typography } from '@material-ui/core';
import React from 'react'

import FotoUsuario from "..//..//..//logo.svg";

const useStyles = makeStyles((theme) => ({
    seccionDesktop :{
        display : "none",
        [theme.breakpoints.up("md")] : {
            display : "flex"
        }
    },
    sessionMobile : {
        display : "flex",
        [theme.breakpoints.up("md")] : {
            display : "none"
        }
    },
    grow : {
        flexGrow : 1
    },
    avatarSize : {
        width : 40,
        height : 40
    }
}));



const BarSesion = () => {


    const classes = useStyles();

    return (
        <Toolbar>
            <IconButton  color = "inherit" >
                <i className = "material-icons">menu</i>
            </IconButton>

            <Typography variant = "h6" >  cursos Online</Typography>

            <div className = {classes.grow}></div>


            <div className = {classes.seccionDesktop}>
            <Button color = "ingerit">
                Salir
            </Button>

            <Button color = "ingerit">
                {"Nombre de Usuario"}
            </Button>
            
            <Avatar src = {FotoUsuario} ></Avatar>

            </div>

           <div  className = {classes.sessionMobile}>
            <IconButton color = "ingerit">
                
            </IconButton>
                <i className = "material-icons" > more_vert</i>
           </div>

        </Toolbar>
    );
};

export default BarSesion;

