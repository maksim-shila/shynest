import { ChangeEvent } from "react";
import { RecipeFormData } from "../models/recipe-form-data"
import { Col, Form, FormGroup, Input, Label, Row } from "reactstrap";

interface Props {
    data: RecipeFormData,
    onChange: (data: RecipeFormData) => unknown
}


export const RecipeForm: React.FC<Props> = ({ data, onChange }) => {

    const handleNameChange = (e: ChangeEvent<HTMLInputElement>) => {
        onChange({ ...data, name: e.target.value });
    }

    const handleDescriptionChange = (e: ChangeEvent<HTMLTextAreaElement>) => {
        onChange({ ...data, description: e.target.value });
    }

    return (
        <Form onSubmit={e => e.preventDefault()}>
            <Row>
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
        </Form>
    );
}