import React from "react";
import { Button } from "reactstrap";
import { RecipesList } from "./components/recipes-list";
import { RecipeViewModal } from "./components/recipe-view-modal";
import { RecipeFormData } from "./models/recipe-form-data";
import { GlobalContext } from "../../context/global-context";
import { useLoader } from "../../../hooks/loader";
import { Recipe } from "../../../api/models";
import { useNavigate } from "react-router-dom";

export const RecipesPage: React.FC = () => {

    const { $api } = React.useContext(GlobalContext);
    const navigate = useNavigate();

    const [recipes, setRecipes] = React.useState<Recipe[]>([]);
    const [activeRecipe, setActiveRecipe] = React.useState<Recipe | null>(null);
    const [isRecipeViewModalOpen, setIsRecipeViewModalOpen] = React.useState(false);

    React.useEffect(() => {
        document.title = "Recipes";
        fetchRecipes();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const fetchRecipes = useLoader(async (): Promise<void> => {
        const request = $api().Recipe.getAll();
        const response = await request.invoke();
        const recipes = response.data;
        if (!response.success || recipes == null) {
            return;
        }
        setRecipes(recipes);
    })

    const toggleRecipeViewModal = () => setIsRecipeViewModalOpen(c => !c);

    const showRecipeViewModal = (recipe: Recipe | null) => {
        setActiveRecipe(recipe);
        setIsRecipeViewModalOpen(true);
    }

    const handleSubmit = useLoader(async (data: RecipeFormData) => {
        const isEdit = activeRecipe !== null;
        if (isEdit && !activeRecipe.id) {
            console.error("Edited recipe hasn't id");
            return;
        }

        const { ...recipe } = data;
        const request = isEdit ? $api().Recipe.post(activeRecipe.id!, recipe) : $api().Recipe.put(recipe);
        const response = await request.invoke();
        const recipeId = response.data?.id;
        if (!recipeId) {
            console.error(`Failed to ${isEdit ? "update" : "create"} recipe`);
            return;
        }
        if (isEdit) {
            setActiveRecipe(null);
            setIsRecipeViewModalOpen(false);
            await fetchRecipes();
        } else {
            navigate(`/recipe/${recipeId}`);
        }
    })

    const deleteRecipe = async (recipe: Recipe) => {
        if (!recipe.id) {
            console.error("Couldn't delete recipe without id");
            return;
        }
        const request = $api().Recipe.delete(recipe.id)
        const response = await request.invoke();
        if (!response.success) {
            return;
        }
        await fetchRecipes();
    }

    return (
        <React.Fragment>
            <Button onClick={() => showRecipeViewModal(null)}>Add Recipe</Button>
            <RecipesList
                recipes={recipes}
                onEdit={showRecipeViewModal}
                onDelete={deleteRecipe}
            />
            <RecipeViewModal
                isOpen={isRecipeViewModalOpen}
                toggle={toggleRecipeViewModal}
                recipe={activeRecipe}
                onSubmit={handleSubmit}
            />
        </React.Fragment>
    )
}