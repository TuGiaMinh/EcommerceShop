import React from "react";
import { Button, Table } from "reactstrap";
import { host } from "../../config";
export default function ListProduct(
    {
        datas,
        onEdit,
        onDelete,
      }
) {
  return (
    <div>
      {!datas && (
        <p className="pt-5 text-center text-uppercase text-secondary">
          No data
        </p>
      )}
      {datas && (
        <Table>
          <thead>
            <tr>
              <th>STT</th>
              <th>Image</th>
              <th>Name</th>
              <th>Category</th>
              <th>Brand</th>
              <th>Price</th>
              <th>Amount</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {datas.map((item, index) => (
              <tr key={+index}>
                <th scope="row">{index + 1}</th>
                <td><img src={!item.images.length ? "" :(host +JSON.parse(JSON.stringify(item.images[0].imageUrl)))} alt={item.name} style={{width:"60px", height:"60px"}} /></td>
                <td>{item.name}</td>
                <td>{JSON.parse(JSON.stringify(item.category.name))}</td>
                <td>{JSON.parse(JSON.stringify(item.brand.name))}</td>
                <td>{item.price}</td>
                <td>{item.amount}</td>
                <td className="text-right">
                  <Button onClick={() => onEdit(item)} color="link">
                    Edit
                  </Button>
                  <Button
                    onClick={() => onDelete(item.productId)}
                    color="link"
                    className="text-danger"
                  >
                    Remove
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      )}
    </div>
  );
}