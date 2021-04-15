import React from "react";
import { Redirect, Route } from "react-router";
import jwt from "jsonwebtoken";

const checkToken = () => {
  var decodedToken = jwt.decode(localStorage.getItem("token"), {
    complete: true,
  });

  if (decodedToken == null) {
    return false;
  }

  let dateNow = new Date();

  if (decodedToken.payload.exp < dateNow.getTime() / 1000) {
    return false;
  } else {
    return true;
  }
};

const AuthenticatedRoute = ({ component: Component, ...rest }) => {
  return (
    <Route
      {...rest}
      render={(props) =>
        checkToken() ? (
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
};

export default AuthenticatedRoute;
