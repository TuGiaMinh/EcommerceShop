import { history } from '../../Helpers/History';
export default function Dashboard(props) {
  const token = localStorage.getItem("token");
  const info = JSON.parse(localStorage.getItem("info"));
  if(!token && !info){
      history.push('/');
  }
    return <p> Hello Admin!!! </p>;
  }