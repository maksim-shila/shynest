import React, { PropsWithChildren } from "react";
import { Spinner } from "reactstrap";
import "./loader.css";

type LoaderContextType = {
    setLoading: (isLoading: boolean) => void;
};

const LoaderContext = React.createContext<LoaderContextType>({
    setLoading: () => { },
});

export const Loader: React.FC<PropsWithChildren<{}>> = ({ children }) => {

    const [counter, setCounter] = React.useState(0);

    const setLoading = (isLoading: boolean): void => {
        setCounter((counter) => {
            const updCounter = isLoading ? counter + 1 : counter - 1;
            if (updCounter < 0) {
                return 0;
            }
            return updCounter;
        });
    };

    return (
        <LoaderContext.Provider value={{ setLoading }}>
            {counter > 0 && <div id="overlay">
                <div id="spinner-container">
                    <Spinner
                        id="spinner"
                        color="secondary"
                    />
                </div>
            </div>}
            {children}
        </LoaderContext.Provider>
    )
}

export const useLoader = <T extends unknown>(callBack: (...args: any[]) => Promise<T>): ((...args: any[]) => Promise<T>) => {
    const { setLoading } = React.useContext(LoaderContext);

    return async (...args) => {
        setLoading(true);
        return await callBack(...args).finally(() => setLoading(false));
    };
};