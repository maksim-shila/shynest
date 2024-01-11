import React, {FormEvent} from "react";
import {useSearchParams} from "react-router-dom";
import {postLogin} from "../api/IdentityApi";

export default function Login() {
    const [searchParams] = useSearchParams();

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const form = event.currentTarget;
        const formData = new FormData(form);

        const data = {
            userName: formData.get("userName") as string,
            password: formData.get("password") as string,
            returnUrl: searchParams.get("returnUrl"),
        }
        const response = await postLogin(data);

        if (!response.success) {
            form.reset();
            return;
        }

        window.location.href = response.returnUrl!;
    }

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="userName">Username: </label>
                <input
                    autoComplete="username"
                    id="userName"
                    name="userName"
                    required
                    type="text"
                />
            </div>

            <div>
                <label htmlFor="password">Password: </label>
                <input
                    autoComplete="current-password"
                    id="password"
                    name="password"
                    required
                    type="password"
                />
            </div>

            <button type="submit">Log in</button>
        </form>
    );
}
