import styles from "./app.module.css";
import "react-toastify/dist/ReactToastify.css";
import { Navigate, Route, Routes } from "react-router";
import { BrowserRouter } from "react-router-dom";
import { NavBar } from "./application/common/navbar";
import { GlobalContextProvider } from "./application/context/global-context";
import { Home } from "./home";
import { Loader } from "./hooks/loader";
import { ProductsPage } from "./application/pages/products/products-page";
import { RecipesPage } from "./application/pages/recipes/recipes-page";
import { RecipePage } from "./application/pages/recipe/recipe-page";
import { ToastContainer } from "react-toastify";
import React from "react";

export const App = () => {
  return (
    <React.Fragment>
      <ToastContainer
        position="bottom-right"
      />
      <Loader>
        <GlobalContextProvider>
          <BrowserRouter>
            <NavBar />
            <div id="app" className={`${styles.appContainer} container`}>
              <Routes>
                <Route path="/" Component={Home} />
                <Route path="/products" Component={ProductsPage} />
                <Route path="/recipes" Component={RecipesPage} />
                <Route path="/recipe/:recipeId" Component={RecipePage} />
                <Route path="*" element={<Navigate to="/" />} />
              </Routes>
            </div>
          </BrowserRouter>
        </GlobalContextProvider>
      </Loader>
    </React.Fragment>
  );
}