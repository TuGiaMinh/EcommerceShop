import React from "react";
import { Button } from "reactstrap";
import  {signoutRedirect,loadUserFromStorage} from "../../Services/authService";


export default function Header() {

  const handleClick = ()=>{
    signoutRedirect();
  }
  const name = localStorage.getItem("name");
  return (
    <div className="clearfix">
      <div className="float-left">
        <img width="40" src="./logo192.png" alt="" />
      </div>
      <div className="float-right">
          <span className="p-5">Hello {name??""}</span> 
        <Button color="danger" onClick={() => handleClick()}>
        Sign Out
      </Button>
      </div>
      
    </div>
  );
}