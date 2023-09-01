import React from "react";
import { Route, Navigate } from "react-router-dom";

const auth = localStorage.getItem("accessToken");

const PrivateRoute = ({ exact, component: Component, ...rest }) => (
  <Route
    exact={exact ? true : false}
    rest
    render={(props) =>
      auth ? (
        <Component {...props} {...rest}></Component>
      ) : (
        <Navigate to={`${process.env.PUBLIC_URL}/login`}></Navigate>
      )
    }
  ></Route>
);

export default PrivateRoute;
