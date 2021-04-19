import React from "react";
import { Button } from "reactstrap";
import brandService from "../../Services/brandService";
import SplitLayout from "../../Components/SplitLayout";
import ListBrand from "../Brands/listBrand";
import EditBrand from "../Brands/editBrand";

export default function Brand(props) {
  const [listBrand, setBrand] = React.useState([]);
  const [itemSelected, setSelected] = React.useState(null);
  React.useEffect(() => {
    handleChange();
  }, []);
  const handleChange = () => {
    brandService.getList().then((res) => {
      setBrand(res.data);
    });
  };
  const handleCreate = () => setSelected({ Name: "" });
  const handleEdit = (item) => setSelected(item);
  const handleCancel = () => setSelected(null);
  const handleDelete = (itemId) => {
    let result = window.confirm("Delete this item?");
    if (result) {
      brandService.delete(itemId).then((res) => {
        setBrand(_removeViewItem(listBrand, itemId));
      });
    }
  };
  const handleSave = (data) => {
    let result = window.confirm("Save the changed items?");
    if (result) {
      if (!data.brandId) {
        brandService.create(data).then((res) => {
          handleChange();
        })
      }
      else {
        brandService.edit(data.brandId, data).then((res) => {
          setBrand(_updateViewItem(listBrand, data));
        });
      }
      setSelected(null);
    }
  };
  const _removeViewItem = (list, itemDel) =>
    list.filter((item) => item.brandId !== itemDel);
  const _updateViewItem = (list, itemEdit) =>
    list.map((item) => (item.brandId === itemEdit.brandId ? itemEdit : item));



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
          datas={listBrand}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onChange={handleChange}
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