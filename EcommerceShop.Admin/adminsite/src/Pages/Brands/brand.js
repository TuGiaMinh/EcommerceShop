import React from "react";
import { Button } from "reactstrap";
import BrandService from "../../Services/brandService";
import SplitLayout from "../../Components/SplitLayout";
import ListBrand from "../Brands/listBrand";
import EditBrand from "../Brands/editBrand";
import { history } from '../../Helpers/History';

export default function Brand() {

  const token = localStorage.getItem("token");
  const info = JSON.parse(localStorage.getItem("info"));

  if(!token && !info){
      history.push('/');
  }

  const [listBrand, setBrand] = React.useState([]);

  const [itemSelected, setSelected] = React.useState(null);

  React.useEffect(() => {
    handleGetBrands();
  }, []);

  const handleGetBrands = () => {
    BrandService.getList().then((res) => {
      setBrand(res.data);
    });
  };

  const handleCreate = () => setSelected({ Name: "" });

  const handleEdit = (item) => setSelected(item);

  const handleSave = (data) => {
    let result = window.confirm("Save the changed items?");
    if (result) {
      if (!data.brandId) {
        BrandService.create(data).then(() => {
          handleGetBrands();
        })
      }
      else {
        BrandService.edit(data.brandId, data).then(() => {
          handleGetBrands();
        });
      }
      setSelected(null);
    }
  };

  const handleDelete = (itemId) => {
    let result = window.confirm("Delete this item?");
    if (result) {
      BrandService.delete(itemId).then(() => {
        handleGetBrands();
      });
    }
  };
  
  const handleCancel = () => setSelected(null);


  return (
    <SplitLayout
      title="Brand"
      actions={
        <Button
          color="primary"
          className="float-right"
          children="New Brand"
          onClick={() => handleCreate()}
        />
      }
      right={
        <ListBrand
          data={listBrand}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onChange={handleGetBrands}
        />
      }
      left={
        <EditBrand
          itemEdit={itemSelected}
          onCancel={handleCancel}
          onSave={handleSave}
        />
      }
    ></SplitLayout>
  );
}