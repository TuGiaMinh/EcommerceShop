import React from "react";
import { Button } from "reactstrap";
import cateService from "../../Services/categoryService";
import SplitLayout from "../../Components/SplitLayout";
import ListCategory from "../Categories/listCategory";
import EditCategory from "../Categories/editCategory";
export default function Category() {
  const [listCategory, setCategory] = React.useState([]);
  const [itemSelected, setSelected] = React.useState(null);
  React.useEffect(() => {
    handleChangeType();
  }, []);
  const handleChangeType = () => {
    cateService.getList().then((resp) => {
      setCategory(resp.data);
    });
  };
  const handleCreate = () => setSelected({ Name: ""});
  const handleEdit = (item) => setSelected(item);
  const handleCancel = () => setSelected(null);
  const handleDelete = (itemId) => {
    let result = window.confirm("Delete this item?");
    if (result) {
      cateService.delete(itemId).then((resp) => {
        setCategory(_removeViewItem(listCategory, itemId));
      });
    }
  };
  const handleSave = (data) => {
    let result = window.confirm("Save the changed items?");
    if (result) {
      if (!data.categoryId) {
        cateService.create(data).then((reps) => {
          handleChangeType();
        });
      } else {
        cateService.edit(data.categoryId, data).then((reps) => {
          setCategory(_updateViewItem(listCategory, data));
        });
      }
      setSelected(null);
    }
  };
  const _removeViewItem = (lists, itemDel) =>
  lists.filter((item) => item.categoryId !== itemDel);

  const _updateViewItem = (lists, itemEdit) =>
  lists.map((item) => (item.categoryId === itemEdit.categoryId ? itemEdit : item));

    return (
      <SplitLayout
        title="Category"
        actions={
          <Button
            color="primary"
            className="float-right"
            children="New Category"
            onClick={() => handleCreate()}
          />
        }
        right={
          <ListCategory
            datas={listCategory}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onChangeType={handleChangeType}
          />
        }
        left={
          <EditCategory
            itemEdit={itemSelected}
            onCancel={handleCancel}
            onSave={handleSave}
          />
        }
      ></SplitLayout>
    );
  ;
}