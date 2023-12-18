import React from "react";
import { ApiClient, IApiClient } from "../../api/api-client";
import { useAuth } from "oidc-react";

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

    const auth = useAuth()

    return (
        <GlobalContext.Provider value={{
            $api: () => new ApiClient(auth.userData?.access_token),
            $user: () => ({ name: auth.userData?.profile.name })
        }}>
            {!auth.isLoading && children}
        </GlobalContext.Provider>
    )
}