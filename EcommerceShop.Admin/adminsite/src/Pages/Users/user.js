import api from'../../Utils/api';
import React from "react";
import { Button, Table } from "reactstrap";
export default function User(props) {
  const [listUser, setListProduct] = React.useState([]);
  React.useEffect(() => {
    handleChange();
  }, []);
  const handleChange = () => {
     api.get("/api/v1/Users/GetAllUser").then(r=>{setListProduct(r.data)});
  };
  console.log(listUser)
    return (
      <div>
         {!listUser && (
        <p className="pt-5 text-center text-uppercase text-secondary">
          No data
        </p>
      )}
      {listUser && (
        <Table>
          <thead>
            <tr>
              <th>STT</th>
              <th>UserName</th>
              <th>Email</th>
              <th>Address</th>
              <th>Phone</th>
              <th>DateofBirth</th>
              <th>Gender</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {listUser.map((item, index) => (
              <tr key={+index}>
                <td scope="row">{index + 1}</td>
                <td>{item.userName}</td>
                <td>{item.email}</td>
                <td>{item.userAddress}</td>
                <td>{item.userTel}</td>
                <td>{item.dateofBirth}</td>
                <td>{item.gender=== true? "Female" : "Male"}</td>
                <td></td>
              </tr>
            ))}
          </tbody>
        </Table>
      )}
      </div>
    );
  }