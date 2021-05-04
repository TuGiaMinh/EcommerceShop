import { history } from './History';
import { Route } from "react-router-dom";
import { loadUserFromStorage, signinRedirect } from "../Services/authService";
import React from 'react';


export default function ProtectedRoute({
  children,
  component: Component,
  ...rest
}) {
  const [user, setUser] = React.useState(null);
  React.useEffect(() => {
    loadUserFromStorage().then(data => {
      if (!data) {

        signinRedirect();
       
      }

      else setUser(data)
    });
  }, [])
  return user && <Route {...rest} component={Component} />;
}