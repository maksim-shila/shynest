import React from "react";
import { ApiClient, IApiClient } from "../../api/api-client";

type User = {
    name?: string;
    token?: string;
}

interface IGlobalContext {
    $api: () => IApiClient;
    $user: () => User | null;
}

export const GlobalContext = React.createContext<IGlobalContext>({
    $api: () => new ApiClient(null),
    $user: () => null
});

interface GlobalContextProviderProps {
    children: React.ReactNode
}

export const GlobalContextProvider: React.FC<GlobalContextProviderProps> = ({ children }) => {

    return (
        <GlobalContext.Provider value={{
            $api: () => new ApiClient(null),
            $user: () => ({ name: "TODO" })
        }}>
            {children}
        </GlobalContext.Provider>
    )
}