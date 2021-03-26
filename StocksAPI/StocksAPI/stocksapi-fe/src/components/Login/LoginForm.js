import { Button } from '@material-ui/core';
import axios from 'axios';
import React, { Component } from 'react';
import { Constants } from '../../constants/Constants';

class LoginForm extends Component{

    async Login(){
        try {
            axios({
                method: 'post',
                url: `${Constants.addStock}`,
                data: {
                    name: "string",
                    purchaseDate: "2021-03-25T11:30:56.023Z",
                    purchasePrice: 0,
                    amount: 0,
                    totalCost: 0,
                    country: "string"
                  }
              });
        } catch (error) {
            console.log(error);
        }
    }

    render() {
        return (
            <form className="centered-box col-lg-12" style={{backgroundColor:"#171123", width:'400px', display:'flex',flexDirection:"column", alignItems:'center', justifyContent:'center', border:'5px solid black'}}>
                <h3 style={{marginTop:'10px', marginBottom:'20px'}}>LOGIN</h3>
                <label>
                    <h6>Username</h6>
                </label>
                <input type="text" name="name" style={{width:'300px'}}/>
                <label style={{ marginTop: '10px' }}>
                    <h6>Password</h6>
                </label>
                <input type="password" name="password" style={{width:'300px'}}/>
                <br/>
                <Button onClick={this.AddStockData} variant="contained" value="Submit" color='secondary' style={{width:'10vw', marginBottom:'10px'}} >
                    Login
                </Button>
            </form>
        );
    }
}

export default LoginForm;