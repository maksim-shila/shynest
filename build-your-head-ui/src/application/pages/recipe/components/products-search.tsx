import React from "react"
import { Button, ListGroup, ListGroupItem } from "reactstrap"
import { Product } from "../../../../api/models";
import { useLoader } from "../../../../hooks/loader";
import { GlobalContext } from "../../../context/global-context";

interface Props {
    onAdd: (product: Product) => unknown
}

export const ProductsSearch: React.FC<Props> = ({ onAdd }) => {

    const { $api } = React.useContext(GlobalContext);
    const [products, setProducts] = React.useState<Product[]>([]);

    React.useEffect(() => {
        fetchProducts();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    const fetchProducts = useLoader(async () => {
        const response = await $api().Product.getAll().invoke();
        if (response.success && response.data) {
            setProducts(response.data)
        }
    })

    return (
        <React.Fragment>
            <ListGroup flush={true}>
                {products.map(product => (
                    <ListGroupItem className="p-0" key={product.id}>
                        <span>{product.name}</span>
                        <Button className="float-end" onClick={() => onAdd(product)}>Add</Button>
                    </ListGroupItem>
                ))}
            </ListGroup>
        </React.Fragment>
    )
}