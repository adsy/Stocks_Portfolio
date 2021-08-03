import React, { useEffect } from "react";
import { Redirect, Route } from "react-router";
import jwt from "jsonwebtoken";
import axios from "axios";
import { Constants } from "../constants/Constants";
import { useState } from "react";

const AuthenticatedRoute = ({ component: Component, ...rest }) => {
  const [authenticating, setAuthenticating] = useState(true);
  const [tokenResult, setTokenResult] = useState();

  const checkToken = () => {
    var decodedToken = jwt.decode(localStorage.getItem("token"), {
      complete: true,
    });

    if (decodedToken == null) {
      return false;
    }

    let dateNow = new Date();

    if (decodedToken.payload.exp < dateNow.getTime() / 1000) {
      // check refreshToken
      axios({
        method: "post",
        url: `${Constants.refreshToken}`,
        data: {
          accessToken: localStorage.getItem("token"),
          refreshToken: localStorage.getItem("refreshToken"),
        },
        headers: { "Access-Control-Allow-Origin": "*" },
      })
        .then((response) => {
          console.log(response);
          if (response.status == 200) {
            console.log("changing token");
            const token = response.data.accessToken;
            const refreshToken = response.data.refreshToken;

            localStorage.setItem("token", token);
            localStorage.setItem("refreshToken", refreshToken);
            setTokenResult(true);
            setAuthenticating(false);
          } else {
            setTokenResult(false);
            setAuthenticating(false);
          }
        })
        .catch((e) => {
          console.log("error caught", e);
          setTokenResult(false);
          setAuthenticating(false);
        });
    } else {
      setTokenResult(true);
      setAuthenticating(false);
    }
  };

  useEffect(() => {
    checkToken();
  }, []);

  if (authenticating) {
    return (
      <div className="div">
        <h5 className="login-loading-center row">🚀</h5>
        <br />
      </div>
    );
  } else {
    return (
      <Route
        {...rest}
        render={(props) =>
          tokenResult ? (
            <Component {...props} />
          ) : (
            <Redirect
              to={{
                pathname: "/login",
                state: { from: props.location },
              }}
            />
          )
        }
      />
    );
  }
};

export default AuthenticatedRoute;
