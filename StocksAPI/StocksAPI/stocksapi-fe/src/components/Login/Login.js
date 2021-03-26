import React from "react";
import LoginForm from "./LoginForm";

const LoginPage = () => {

    return(

        <div className="centered-box" style={{height:'100vh'}}>
            <div className="centered-box" style={{display:'flex', justifyContent:'center', alignContent:'center', }}>
                <LoginForm/>
            </div>
        </div>

    )
}

export default LoginPage;