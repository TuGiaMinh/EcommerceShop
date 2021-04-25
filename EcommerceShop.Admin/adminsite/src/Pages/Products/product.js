import React, { useState } from "react";
import { Button,Row,Col } from "reactstrap";
import ProductService from "../../Services/productService";
import ListProduct from "../Products/listProduct";
import EditProduct from "../Products/editProduct";
import { history } from '../../Helpers/History';
export default function Product() {

  const token = localStorage.getItem("token");
  const info = JSON.parse(localStorage.getItem("info"));

  if(!token && !info){
      history.push('/');
  }

  const [listProduct, setListProduct] = useState([]);

  const [itemSelected, setSelected] = useState(null);

  React.useEffect(() => {
    handleGetProducts();
  }, []);

  const handleGetProducts = () => {
    ProductService.getList().then((res) => {
      setListProduct(res.data);
    });
  };

  const handleCreate = () => setSelected({ Name: "", Category:"",Brand:"",Price:0,amount:0,Description:""});

  const handleEdit = (item) => { setSelected(item) };

  const handleSave = (data) => {
    let result = window.confirm("Save the changed items?");
    if (result) {
      if (!itemSelected.productId) {
       
        ProductService.create(data).then(() => {
          handleGetProducts();
        });
      } 
      else {
        ProductService.edit(itemSelected.productId, data).then(() => {
          handleGetProducts();
        });
      }
      setSelected(null);
    }
  };

  const handleDelete = (itemId) => {
    let result = window.confirm("Delete this item?");
    if (result) {
      ProductService.delete(itemId).then(() => {
        handleGetProducts();
      });
    }
  };

  const handleCancel = () => setSelected(null);

  return (
    <div>
        <Button
        color="primary"
        className="ml-3 mb-3"
        children="New Category"
        onClick={() => handleCreate()}
      />
      <EditProduct
        itemEdit={itemSelected}
        onCancel={handleCancel}
        onSave={handleSave}
      />
      <ListProduct
        data={listProduct}
        onEdit={handleEdit}
        onDelete={handleDelete}
        onChange={handleGetProducts}
      />
      
    </div>
  )
}