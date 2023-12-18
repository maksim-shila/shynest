import React, { ChangeEvent } from "react";
import { Button, Col, Form, FormGroup, Input, InputGroup, Label, Row } from "reactstrap"
import { AvatarUpload } from "../../../common/avatar-upload";
import { ProductFormData } from "../models/product-form-data";

interface Props {
    data: ProductFormData,
    onChange: (data: ProductFormData) => unknown
}

export const ProductForm: React.FC<Props> = ({ data, onChange }) => {

    const handleNameChange = (e: ChangeEvent<HTMLInputElement>) => {
        onChange({ ...data, name: e.target.value });
    }

    const handleDescriptionChange = (e: ChangeEvent<HTMLTextAreaElement>) => {
        onChange({ ...data, description: e.target.value });
    }

    const handleCarbohydratesChange = (e: ChangeEvent<HTMLInputElement>) => {
        onChange({ ...data, carbohydrates: Number(e.target.value) });
    }

    const handleProteinsChange = (e: ChangeEvent<HTMLInputElement>) => {
        onChange({ ...data, proteins: Number(e.target.value) });
    }

    const handleFatsChange = (e: ChangeEvent<HTMLInputElement>) => {
        onChange({ ...data, fats: Number(e.target.value) });
    }

    const handleNutritionChange = (e: ChangeEvent<HTMLInputElement>) => {
        onChange({ ...data, nutrition: Number(e.target.value) });
    }

    const handleImageChange = (imageBase64: string) => {
        onChange({ ...data, imageBase64, imageChanged: true });
    }

    const calculateNutrition = () => {
        const nutrition = data.proteins * 4 + data.carbohydrates * 4 + data.fats * 9;
        onChange({ ...data, nutrition: nutrition });
    }

    return (
        <Form onSubmit={e => e.preventDefault()}>
            <Row>
                <Col md={4}>
                    <AvatarUpload initial={data.imageBase64} onChange={handleImageChange} />
                </Col>
                <Col>
                    <FormGroup>
                        <Label for="name">Name:</Label>
                        <Input
                            id="name"
                            name="name"
                            autoComplete="off"
                            value={data?.name}
                            onChange={handleNameChange}
                        />
                    </FormGroup>
                    <FormGroup>
                        <Label for="description">Description:</Label>
                        <textarea
                            id="description"
                            name="description"
                            className="textarea form-control"
                            rows={6}
                            value={data.description}
                            onChange={handleDescriptionChange}
                        />
                    </FormGroup>
                </Col>
            </Row>

            <Row>
                <Col md={2}>
                    <Label for="carbohydrates">Carbs:</Label>
                    <Input
                        id="carbohydrates"
                        name="carbohydrates"
                        type="number"
                        value={data.carbohydrates}
                        onChange={handleCarbohydratesChange}
                    />
                </Col>
                <Col md={2}>
                    <Label for="proteins">Proteins:</Label>
                    <Input
                        id="proteins"
                        name="proteins"
                        type="number"
                        value={data.proteins}
                        onChange={handleProteinsChange}
                    />
                </Col>
                <Col md={2}>
                    <Label for="fats">Fats:</Label>
                    <Input
                        name="fats"
                        id="fats"
                        type="number"
                        value={data.fats}
                        onChange={handleFatsChange}
                    />
                </Col>
                <Col md={2} />
                <Col md={4}>
                    <Label for="nutrition">Nutrition:</Label>
                    <InputGroup>
                        <Input
                            name="nutrition"
                            id="nutrition"
                            type="number"
                            value={data.nutrition}
                            onChange={handleNutritionChange}
                        />
                        <Button onClick={calculateNutrition}>Calculate</Button>
                    </InputGroup>
                </Col>
            </Row>
        </Form>
    )
}