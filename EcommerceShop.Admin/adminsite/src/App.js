import { Route, Router,Switch} from "react-router-dom";
import Header from "./Components/Header";
import Navigate from "./Components/Navigate";
import PageLayout from "./Components/PageLayout";
import Routes from "./routes";
import { history } from './Helpers/History';
import React from "react";
import  {loadUserFromStorage} from "./Services/authService";

function App() {

  loadUserFromStorage().then(token => {
    localStorage.setItem("token",token?.access_token);
   });  
   return (
    <Router history={history}>
        <Switch>
        <PageLayout header={<Header />} nav={<Navigate />} content={<Routes />} />
      </Switch>
    </Router>
  );
}

export default App;
