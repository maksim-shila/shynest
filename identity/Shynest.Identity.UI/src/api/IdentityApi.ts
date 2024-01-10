type LoginRequest = {
    password: string;
    userName: string;
    returnUrl: string | null;
}

type LoginResponse = {
    ok: boolean;
    returnUrl?: string;
}

export async function postLogin(data: LoginRequest): Promise<LoginResponse> {
    const response = await fetch("/api/login", {
        body: JSON.stringify(data),
        headers: {
            "Content-Type": "application/json",
        },
        method: "POST",
    });

    if (!response.ok) {
        return {ok: response.ok}
    }

    const {returnUrl} = await response.json()
    return {
        ok: response.ok,
        returnUrl: returnUrl
    }
}
