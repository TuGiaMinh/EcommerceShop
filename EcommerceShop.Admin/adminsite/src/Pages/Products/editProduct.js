import React from "react";
import { Button, Form, FormGroup, Label, Input, Col, Row } from "reactstrap";
import brandService from "../../Services/brandService";
import cateService from "../../Services/categoryService";

export default function EditProduct({ itemEdit, onSave, onCancel }) {

    const itemId = itemEdit?.productId ?? 0;

    React.useEffect(() => {
        cateService.getList().then((res) => {
            setCategory(res.data);
        });
        brandService.getList().then((res) => {
            setBrand(res.data);
        });
        setInput({
            Name: itemEdit?.name,
            CategoryId: itemEdit?.categoryId,
            BrandId: itemEdit?.brandId,
            Price: itemEdit?.price,
            Amount: itemEdit?.amount,
            Description: itemEdit?.description,
        })
    }, [itemEdit]);

    const [listCategory, setCategory] = React.useState([]);

    const [listBrand, setBrand] = React.useState([]);

    const [input, setInput] = React.useState(
        {
            Name: "",
            CategoryId: 1,
            BrandId: 1,
            Price: 0,
            Amount: 0,
            Description: "",
            
        });

    const [files, setFiles] = React.useState([]);

    const handleChangeProduct = (e) => {
        const value = e.target.value;
        setInput({
            ...input,
            [e.target.name]: value,
        });
        let images = []
        if (e.target.files) {

            for (let i = 0; i < e.target.files.length; i++) {
                images.push(e.target.files[i]);
            }
            setFiles(images);
        }
    };

    const ProductFormData = (product, listImage) => {
        let myFormData = new FormData();
        myFormData.append("name", product?.Name);
        myFormData.append("price", product?.Price);
        myFormData.append("amount", product?.Amount);
        myFormData.append("description", product?.Description);
        myFormData.append("categoryId",product?.CategoryId);
        myFormData.append("brandId", product?.BrandId);
        if (listImage) {
            [...listImage].forEach((file) => {
                myFormData.append("images", file);
            });
        }
        return myFormData;
    };

    const handleSubmit = () => {

        if (input && files) {
            onSave(ProductFormData(input, files));
            setFiles([]);
        }
        else window.alert("Please fill the form below");
    };
    
    const itemsCategory = () => {
        return listCategory?.map((item) => {
            return <option value={item.categoryId} selected={item.categoryId === itemEdit.category?.categoryId ? true : false}>{item.name}</option>;
        });
    }

    const itemsBrand = () => {
        return listBrand?.map((item) => {
            return <option value={item.brandId} selected={item.brandId === itemEdit.brand?.brandId ? true : false}>{item.name}</option>;
        });
    }

    return (
        <div>
            {!itemEdit && (
                <p className="pt-5 text-center text-uppercase text-primary">
                    Select item
                </p>
            )}
            {itemEdit && (

                <Form className="mb-5" onSubmit={handleSubmit}>
                    <Row>
                        <Col lg={3} className="text-center">
                            <FormGroup>
                                <Label for="Name">Name</Label>
                                <Input
                                    invalid={!input}
                                    type="text"
                                    name="Name"
                                    id="Name"
                                    onChange={handleChangeProduct}
                                    defaultValue={input.Name}
                                />
                            </FormGroup>
                        </Col>
                        <Col lg={3} className="text-center">
                            <FormGroup>
                                <Label for="Description">Description</Label>
                                <Input
                                    invalid={!input}
                                    type="text"
                                    name="Description"
                                    id="Description"
                                    onChange={handleChangeProduct}
                                    defaultValue={input.Description}
                                />
                            </FormGroup>

                        </Col>
                    </Row>
                    <Row>
                        <Col lg={3} className="text-center">
                            <FormGroup>

                                <Label for="Category">Category</Label>
                                <select name="CategoryId" onChange={handleChangeProduct} style={{
                                    width: "100%", height: "100%", padding: ".375rem .75rem",
                                    border: "1px solid #ced4da", backgroundClip: "padding-box", borderRadius: ".25rem",
                                    fontSize: "1rem",
                                    fontWeight: "400",
                                    lineHeight: "1.5",
                                }}>
                                    {itemsCategory()}
                                </select>
                            </FormGroup>

                        </Col>
                        <Col lg={3} className="text-center">
                            <FormGroup>
                                <Label for="Brand">Brand</Label>
                                <select name="BrandId" onChange={handleChangeProduct} style={{
                                    width: "100%", height: "100%", padding: ".375rem .75rem",
                                    border: "1px solid #ced4da", backgroundClip: "padding-box", borderRadius: ".25rem",
                                    fontSize: "1rem",
                                    fontWeight: "400",
                                    lineHeight: "1.5",
                                }}>
                                    {itemsBrand()}
                                </select>
                            </FormGroup>

                        </Col>
                    </Row>
                    <Row>
                        <Col lg={3} className="text-center">
                            <FormGroup>
                                <Label for="Price">Price</Label>
                                <Input
                                    invalid={!input}
                                    type="number"
                                    name="Price"
                                    id="Price"
                                    onChange={handleChangeProduct}
                                    defaultValue={input.Price}
                                />
                            </FormGroup>

                        </Col>
                        <Col lg={3} className="text-center">
                            <FormGroup>
                                <Label for="Amount">Amount</Label>
                                <Input
                                    invalid={!input}
                                    type="number"
                                    name="Amount"
                                    id="Amount"
                                    onChange={handleChangeProduct}
                                    defaultValue={input.Amount}
                                />
                            </FormGroup>
                        </Col>
                    </Row>
                    <Row>
                        <FormGroup className="ml-3">
                            <Label for="File">File</Label>
                            <Input type="file" multiple onChange={handleChangeProduct} />
                        </FormGroup>
                    </Row>
                    <Row>
                        <Col lg={3} >
                            <FormGroup>
                                <Button color="primary" className="float-right" onClick={handleSubmit}>
                                    Save
                        </Button>
                            </FormGroup>
                        </Col>
                        <Col lg={3} >
                            <FormGroup>
                                <Button color="danger" className="float-left" onClick={() => onCancel()}>
                                    Cancel
                                     </Button>
                            </FormGroup>
                        </Col>
                    </Row>
                </Form>
            )}{" "}
        </div>
    );
}