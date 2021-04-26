import { history } from './History';
import { Route} from "react-router-dom";



export default function ProtectedRoute({
    children,
    component: Component,
    ...rest
  }) {
    const token = localStorage.getItem("token");
    const info = JSON.parse(localStorage.getItem("info"));
  
    if(!token && !info){
        history.push('/');
    }
  
    return token && <Route {...rest} component={Component} />;
  }