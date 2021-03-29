import { Button } from '@material-ui/core';
import axios from 'axios';
import React, { Component, setState } from 'react';
import { Router, useHistory } from 'react-router';
import { Constants } from '../../constants/Constants';

class LoginForm extends Component {
    constructor(props) {
        super(props);
        this.state = { email: '', password: '', errorMessage: null, };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleEmailChange = this.handleEmailChange.bind(this);
        this.handlePasswordChange = this.handlePasswordChange.bind(this);
    }

    async Login() {
        axios({
            method: 'post',
            url: `https://localhost:44345/api/Account/Login`,
            data: {
                email: `${this.state.email}`,
                password: `${this.state.password}`
            }
        }).then(response => {
            
            localStorage.removeItem('token');
            // 10 minutes from now.
            var time = Date.now()+3600000;
            localStorage.setItem('token-expire', time.toString())
            const token = response.data.token;
            localStorage.setItem('token', token);
            

            this.props.history.push("/dashboard")

        }).catch(error => {
            console.log(error);
            this.setState({errorMessage:"Error logging in - Incorrect username or password"});
        });
    }


    handleEmailChange(event) {
        this.setState({ email: event.target.value })
    }

    handlePasswordChange(event) {
        this.setState({ password: event.target.value })
    }


    async handleSubmit(event) {
        event.preventDefault();

        await this.Login();
        console.log(this.props.history);
    }

    render() {
        return (
            <form className="centered-box col-lg-12" style={{ borderRadius:'0px 50px', backgroundColor: "#171123", width: '400px', display: 'flex', flexDirection: "column", alignItems: 'center', justifyContent: 'center', border: '5px solid black' }}>
                <h3 style={{ marginTop: '20px', marginBottom: '40px', fontSize:'42px' }}>LOGIN</h3>
                {this.state.errorMessage != null ? <p style={{color:'red'}}> {this.state.errorMessage} </p> : ""}
                <label>
                    <h6>Email</h6>
                </label>
                <input type="email" name="email" value={this.state.email} onChange={this.handleEmailChange} style={{ width: '300px' }} />
                <label style={{ marginTop: '10px' }}>
                    <h6>Password</h6>
                </label>
                <input type="password" name="password" value={this.state.password} onChange={this.handlePasswordChange} style={{ width: '300px' }} />
                <br />
                <Button onClick={this.handleSubmit} variant="contained" value="Submit" color='secondary' style={{ width: '10vw', marginBottom: '10px' }} >
                    Login
                </Button>
            </form>
        );
    }
}

export default LoginForm;