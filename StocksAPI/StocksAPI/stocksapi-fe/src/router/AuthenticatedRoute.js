import React, { useEffect } from "react";
import { Redirect, Route } from "react-router";
import jwt from "jsonwebtoken";
import axios from "axios";
import { Constants } from "../constants/Constants";
import { useState } from "react";
import Cookies from "js-cookie";

const AuthenticatedRoute = ({ component: Component, ...rest }) => {
  const [authenticating, setAuthenticating] = useState(true);
  const [tokenResult, setTokenResult] = useState();

  const checkToken = () => {
    var decodedToken = jwt.decode(localStorage.getItem("token"), {
      complete: true,
    });

    if (decodedToken == null) {
      setAuthenticating(false);
      setTokenResult(false);
    } else {
      let dateNow = new Date();

      if (decodedToken.payload.exp < dateNow.getTime() / 1000) {
        // check refreshToken
        axios.defaults.withCredentials = true;
        axios({
          method: "post",
          url: `${Constants.refreshToken}`,
          data: {
            accessToken: localStorage.getItem("token"),
            refreshToken: localStorage.getItem("refreshToken"),
          },
        })
          .then((response) => {
            if (response.status == 200) {
              const token = response.data.accessToken;
              localStorage.setItem("token", token);
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
    }
  };

  useEffect(() => {
    checkToken();
  }, []);

  if (authenticating) {
    return <div>test</div>;
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
