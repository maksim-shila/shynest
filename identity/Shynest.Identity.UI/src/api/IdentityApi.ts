import axios, { CreateAxiosDefaults } from "axios";
import { STATUS_CODES } from "http";

type LoginRequest = {
    password: string;
    userName: string;
    returnUrl: string | null;
}

type LoginResponse = {
    success: boolean;
    returnUrl?: string;
}

const config: CreateAxiosDefaults = {
    baseURL: process.env.REACT_APP_API_URL,
    headers: {
        get: { "Accept": "application/json" },
        post: { "Content-Type": "application/json" },
        put: { "Content-Type": "application/json" }
    }
}

const client = axios.create(config); 

export async function postLogin(data: LoginRequest): Promise<LoginResponse> {
    const response = await client.post('/login', data);

    if (response.status != 200) {
        return { success: false }
    }

    const { returnUrl } = response.data;
    return {
        success: true,
        returnUrl: returnUrl
    }
}