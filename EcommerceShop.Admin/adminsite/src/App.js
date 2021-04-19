import { BrowserRouter as Router } from "react-router-dom";
import Header from "./Components/Header";
import Navigate from "./Components/Navigate";
import PageLayout from "./Components/PageLayout";
import Routes from "./routes";

function App() {
  return (
  <Router>
    <PageLayout header={<Header />} nav={<Navigate />} content={<Routes />} />
  </Router>
  );
}

export default App;
