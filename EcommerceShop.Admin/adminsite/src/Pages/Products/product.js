import React, { useState } from "react";
import { Button,Row,Col } from "reactstrap";
import productService from "../../Services/productService";
import ListProduct from "../Products/listProduct";
import EditProduct from "../Products/editProduct";
export default function Product() {
  const [listProduct, setListProduct] = useState([]);
  const [itemSelected, setSelected] = useState(null);
  React.useEffect(() => {
    handleChange();
  }, []);
  const handleChange = () => {
    productService.getList().then((res) => {
      setListProduct(res.data);
    });
  };
  const handleCreate = () => setSelected({ Name: "", Category:"",Brand:"",Price:0,amount:0,Description:""});
  const handleCancel = () => setSelected(null);
  const handleEdit = (item) => {
    setSelected(item);
  };
  const handleSave = (data) => {
    let result = window.confirm("Save the changed items?");
    if (result) {
      if (!itemSelected.productId) {
        productService.create(data).then((reps) => {
          handleChange();
        });
      } 
      else {
        productService.edit(itemSelected.productId, data).then((reps) => {
          handleChange();
        });
      }
      setSelected(null);
    }
  };
  const handleDelete = (itemId) => {
    let result = window.confirm("Delete this item?");
    if (result) {
      productService.delete(itemId).then((resp) => {
        setListProduct(_removeViewItem(listProduct, itemId));
      });
    }
  };
  const _removeViewItem = (lists, itemDel) =>
  lists.filter((item) => item.productId !== itemDel);
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
        datas={listProduct}
        onEdit={handleEdit}
        onDelete={handleDelete}
        onChange={handleChange}
      />
      
    </div>
  )
}