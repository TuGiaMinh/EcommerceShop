import React from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";

export default function EditBrand({ itemEdit, onSave, onCancel }) {
    const itemId = itemEdit?.brandId ?? 0;
    const [inputName, setInputName] = React.useState("");
    React.useEffect(() => {
        setInputName(itemEdit?.name);
    }, [itemEdit]);
    const handleChangeName = (e) => setInputName(e.target.value);
    const handleSubmit = () => {
        if (inputName)
            onSave({ brandId: itemId, name: inputName });
        else window.alert("Please fill the form below");
    }
    return (
        <div>
            {
                !itemEdit && (
                    <p className="pt-5 text-center text-uppercase text-secondary">
                        Select item
                    </p>
                )
            }
            {
                itemEdit && (
                    <Form>
                        <FormGroup>
                            <Label for="Name">Brand Name</Label>
                            <Input
                                invalid={!inputName}
                                type="text"
                                name="Name"
                                id="Name"
                                onChange={handleChangeName}
                                value={inputName}
                            >
                            </Input>
                        </FormGroup>
                        <Button
                            color="primary" className="mr-3" onClick={handleSubmit}
                        >
                            Save
                        </Button>
                        <Button color="danger" onClick={() => onCancel()}>
                            Cancel
                        </Button>
                    </Form>
                )
            }{""}
        </div>
    );
}