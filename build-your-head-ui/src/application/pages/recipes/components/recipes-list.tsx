import { Button, ButtonGroup, Table } from "reactstrap";
import { Recipe } from "../../../../api/models";
import { useNavigate } from "react-router-dom";

interface Props {
    recipes: Recipe[],
    onEdit: (recipe: Recipe) => unknown,
    onDelete: (recipe: Recipe) => unknown
}

export const RecipesList: React.FC<Props> = ({ recipes, onEdit, onDelete }) => {

    const navigate = useNavigate();

    return (
        <div>
            {recipes && recipes.length > 0 &&
                <Table striped className="products-table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                            <th />
                        </tr>
                    </thead>
                    <tbody>
                        {recipes.map(recipe => (
                            <tr key={recipe.id}>
                                <td className="cell-name">{recipe.name}</td>
                                <td className="cell-description">{recipe.description}</td>
                                <td>
                                    <ButtonGroup>
                                        <Button color="warning" onClick={() => onEdit(recipe)}>Edit</Button>
                                        <Button color="dark" onClick={() => onDelete(recipe)}>Delete</Button>
                                        <Button color="success" onClick={() => navigate(`/recipe/${recipe.id}`)}>View</Button>
                                    </ButtonGroup>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>}
        </div>
    );
}