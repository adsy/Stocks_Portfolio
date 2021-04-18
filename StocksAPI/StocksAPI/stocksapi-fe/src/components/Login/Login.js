import React from "react";
import { useHistory } from "react-router";
import LoginForm from "./LoginForm";

const LoginPage = () => {
  var history = useHistory();

  return (
    <div className="centered-box" style={{ height: "100vh" }}>
      <div
        className="centered-box"
        style={{
          display: "flex",
          justifyContent: "center",
          alignContent: "center",
        }}
      >
        <LoginForm history={history} />
      </div>
    </div>
  );
};

export default LoginPage;
