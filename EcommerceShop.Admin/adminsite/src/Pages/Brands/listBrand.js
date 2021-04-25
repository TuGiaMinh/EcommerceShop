import React from "react";
import { Button, Table } from "reactstrap";
export default function ListBrand({ data, onEdit, onDelete, })
{
    return (
        <div>
            {!data &&(
              <p className="pt-5.text-center.text-uppercase.text-secondary">No data</p> 
            )}
            {
                data &&(
                    <Table>
                        <thead>
                            <tr>
                                <th>STT</th>
                                <th>Category</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                data.map((item,index)=>(
                                    <tr key={+index}>
                                        <td scope="row">{index+1}</td>
                                        <td>{item.name}</td>
                                        <td className="text-right">
                                        <Button onClick={()=>onEdit(item)} color="link">Edit</Button>
                                        <Button onClick={()=>onDelete(item.brandId)} color="link" className="text-danger">Remove</Button>
                                        </td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </Table>
                )
            }
        </div>
    );
}