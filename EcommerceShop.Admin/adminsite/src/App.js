import { Router,Switch} from "react-router-dom";
import Header from "./Components/Header";
import Navigate from "./Components/Navigate";
import PageLayout from "./Components/PageLayout";
import Routes from "./routes";
import {Login} from"./Pages/Users/Login";
import { history } from './Helpers/History';


function App() {
  return (
    <Router history={history}>
        <Switch>
        <Login exact path="/"/>
        <PageLayout header={<Header />} nav={<Navigate />} content={<Routes />} />
      </Switch>
    </Router>
  );
}

export default App;
