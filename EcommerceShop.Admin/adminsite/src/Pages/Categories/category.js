import React from "react";
import { Button } from "reactstrap";
import CategoryService from "../../Services/categoryService";
import SplitLayout from "../../Components/SplitLayout";
import ListCategory from "../Categories/listCategory";
import EditCategory from "../Categories/editCategory";
export default function Category() {
  const [listCategory, setCategories] = React.useState([]);

  const [itemSelected, setSelected] = React.useState(null);

  React.useEffect(() => {
    handleGetCategories();
  }, []);

  const handleGetCategories = () => {
    CategoryService.getList().then((res) => {
      setCategories(res.data);
    });
  };

  const handleCreate = () => setSelected({ Name: ""});

  const handleEdit = (item) => setSelected(item);
  
  const handleSave = (data) => {
    let result = window.confirm("Save the changed items?");
    if (result) {
      if (!data.categoryId) {
        CategoryService.create(data).then(() => {
          handleGetCategories();
        });
      } else {
        CategoryService.edit(data.categoryId, data).then(() => {
          handleGetCategories();
        });
      }
      setSelected(null);
    }
  };

  const handleDelete = (itemId) => {
    let result = window.confirm("Delete this item?");
    if (result) {
      CategoryService.delete(itemId).then(() => {
        handleGetCategories();
      });
    }
  };

  const handleCancel = () => setSelected(null);

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
            data={listCategory}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onChange={handleGetCategories}
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