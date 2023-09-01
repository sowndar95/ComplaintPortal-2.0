import React from "react";
import { Route, Redirect } from "react-router-dom";

const userContext = localStorage.getItem("userContext") ? JSON.parse(localStorage.getItem("userContext")) : null;

const ProtectedRoute = ({ exact, allowedPermission, component: Component, ...rest }) => (
  <Route
    exact={exact ? true : false}
    rest
    render={(props) =>
      ( (userContext?.permission & allowedPermission) != 0 ) ? (
        <Component {...props} {...rest}></Component>
      ) : (
        <Redirect to={`${process.env.PUBLIC_URL}/accessdenied`}></Redirect>
      )
    }
  ></Route>
);

export default ProtectedRoute;
