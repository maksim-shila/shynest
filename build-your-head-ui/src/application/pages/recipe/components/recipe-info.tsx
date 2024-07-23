import React from "react"
import { Recipe } from "../../../../api/models"

interface Props {
    recipe: Recipe
}

export const RecipeInfo: React.FC<Props> = ({ recipe }) => {
    return (
        <React.Fragment>
            <h1>{recipe.name}</h1>
            <p>{recipe.description}</p>
        </React.Fragment>
    )
}