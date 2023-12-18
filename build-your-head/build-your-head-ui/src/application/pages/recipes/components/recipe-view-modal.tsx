import React, { FormEvent } from "react";
import { RecipeFormData } from "../models/recipe-form-data";
import { RecipeForm } from "./recipe-form";
import { Recipe } from "../../../../api/models";
import { Button, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";

const defaultData: RecipeFormData = {
    name: "",
    description: ""
}

interface Props {
    isOpen: boolean,
    toggle: () => unknown,
    recipe: Recipe | null,
    onSubmit: (recipe: RecipeFormData) => unknown
}

export const RecipeViewModal: React.FC<Props> = ({ isOpen, toggle, recipe, onSubmit }) => {

    const isEdit = recipe !== null;

    const [data, setData] = React.useState<RecipeFormData>(defaultData);

    React.useEffect(() => {
        if (!isOpen) {
            return;
        }

        (async () => {
            const data: RecipeFormData = {
                name: recipe?.name ?? defaultData.name,
                description: recipe?.description ?? defaultData.description
            }
            setData(data);
        })();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isOpen, recipe])

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        onSubmit(data);
    }

    return (
        <Modal id="recipeModal" isOpen={isOpen} toggle={toggle} size="lg">
            <ModalHeader toggle={toggle}>{isEdit ? "Edit Recipe" : "Add Recipe"}</ModalHeader>
            <ModalBody>
                <RecipeForm data={data} onChange={setData} />
            </ModalBody>
            <ModalFooter>
                <Button className="btn-success mt-4" onClick={handleSubmit}>{isEdit ? "Update" : "Add"}</Button>
            </ModalFooter>
        </Modal>)
}