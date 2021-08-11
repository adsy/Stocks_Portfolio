import React from "react";
import { useHistory } from "react-router";
import LoginForm from "./LoginForm";

const LoginPage = () => {
  var history = useHistory();

  return (
    <div className="centered-box" style={{ height: "100vh" }}>
      <div class="header-container">
        <div class="text-box">
          <h5 className="topline">Sir this is a Wendy's..</h5>
          <h5 className="topline-2">Sir this is a Wendy's..</h5>
        </div>
      </div>
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
