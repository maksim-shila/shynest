import React from "react";
import { Recipe } from "../../../../api/models";
import { Button } from "reactstrap";

interface Props {
    recipe: Recipe
}

export const RecipeSteps: React.FC<Props> = ({ recipe }) => {
    return (
        <React.Fragment>
            <h2>Steps</h2>
            <Button>Add Step</Button>
        </React.Fragment>
    )
}