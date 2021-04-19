import React from "react";
import { Button, Table } from "reactstrap";
export default function ListBrand({ datas, onEdit, onDelete, })
{
    return (
        <div>
            {!datas &&(
              <p className="pt-5.text-center.text-uppercase.text-secondary">No data</p> 
            )}
            {
                datas &&(
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
                                datas.map((item,index)=>(
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