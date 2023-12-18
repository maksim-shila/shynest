import React, { FormEvent } from "react";
import { Button, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import { Product } from "../../../../api/models";
import { GlobalContext } from "../../../context/global-context";
import { ProductForm } from "./product-form";
import { ProductFormData } from "../models/product-form-data";

const defaultData: ProductFormData = {
    name: "",
    description: "",
    proteins: 0,
    carbohydrates: 0,
    fats: 0,
    nutrition: 0,
    imageBase64: null,
    imageChanged: false
}

interface Props {
    isOpen: boolean,
    toggle: () => unknown,
    product: Product | null,
    onSubmit: (product: ProductFormData) => unknown
}

export const ProductViewModal: React.FC<Props> = ({ isOpen, toggle, product, onSubmit }) => {

    const isEdit = product !== null;

    const { $api } = React.useContext(GlobalContext);
    const [data, setData] = React.useState<ProductFormData>(defaultData);

    React.useEffect(() => {
        if (!isOpen) {
            return;
        }

        (async () => {
            const data: ProductFormData = {
                name: product?.name ?? defaultData.name,
                description: product?.description ?? defaultData.description,
                proteins: product?.proteins ?? defaultData.proteins,
                carbohydrates: product?.carbohydrates ?? defaultData.carbohydrates,
                fats: product?.fats ?? defaultData.fats,
                nutrition: product?.nutrition ?? defaultData.nutrition,
                imageBase64: defaultData.imageBase64,
                imageChanged: defaultData.imageChanged
            }
            if (product?.id) {
                const imageBase64 = await fetchImage(product.id);
                data.imageBase64 = imageBase64;
            }
            setData(data);
        })();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isOpen, product])

    const fetchImage = async (productId: number): Promise<string | null> => {
        const response = await $api().Product.getPrimaryImage(productId).invoke();
        return response.data;
    }

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        onSubmit(data);
    }

    return (
        <Modal id="productModal" isOpen={isOpen} toggle={toggle} size="lg">
            <ModalHeader toggle={toggle}>{isEdit ? "Edit Product" : "Add Product"}</ModalHeader>
            <ModalBody>
                <ProductForm data={data} onChange={setData} />
            </ModalBody>
            <ModalFooter>
                <Button className="btn-success mt-4" onClick={handleSubmit}>{isEdit ? "Update" : "Add"}</Button>
            </ModalFooter>
        </Modal>)
}