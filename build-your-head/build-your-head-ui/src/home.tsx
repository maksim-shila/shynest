import React from "react";

export const Home: React.FC = () => {

    React.useEffect(() => {
        document.title = "Build Your Head";
    })

    return (
        <div>What a wonderful world</div>
    );
}