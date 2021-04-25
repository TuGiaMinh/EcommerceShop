import React from "react";
import { Button } from "reactstrap";
import { history } from '../../Helpers/History';


export default function Header(props) {
  const token = localStorage.getItem("token");
  const info = JSON.parse(localStorage.getItem("info"));
  const handleClick = ()=>{
    localStorage.removeItem("token");
    localStorage.removeItem("info");

    history.push("/");
  };
  return (
    <div className="clearfix">
      <div className="float-left">
        <img width="40" src="./logo192.png" alt="" />
      </div>
      <div className="float-right">
          <span className="p-5">Hello {info?.userName ? info.userName : ""}</span> 
        <Button color="danger" onClick={() => handleClick()}>
        Sign Out
      </Button>
      </div>
      
    </div>
  );
}