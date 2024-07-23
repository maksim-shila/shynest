import React from "react";
import { Button, ButtonGroup, Table } from "reactstrap";
import { Product } from "../../../../api/models";

interface ProductsListProps {
    products: Product[];
    onEdit: (product: Product) => unknown,
    onDelete: (product: Product) => unknown
}

export const ProductsList: React.FC<ProductsListProps> = ({ products, onEdit, onDelete }) => {

    return (
        <div>
            {products && products.length > 0 &&
                <Table striped className="products-table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Carbs</th>
                            <th>Proteins</th>
                            <th>Fats</th>
                            <th>Nutrition</th>
                            <th />
                        </tr>
                    </thead>
                    <tbody>
                        {products.map(product => (
                            <tr key={product.id}>
                                <td className="cell-name">{product.name}</td>
                                <td className="cell-description">{product.description}</td>
                                <td className="cell-carbohydrates">{product.carbohydrates}</td>
                                <td className="cell-proteins">{product.proteins}</td>
                                <td className="cell-fats">{product.fats}</td>
                                <td className="cell-nutrition">{product.nutrition}</td>
                                <td>
                                    <ButtonGroup>
                                        <Button color="warning" onClick={() => onEdit(product)}>Edit</Button>
                                        <Button color="dark" onClick={() => onDelete(product)}>Delete</Button>
                                    </ButtonGroup>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>}
        </div>
    );
}