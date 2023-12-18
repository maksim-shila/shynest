import axios, { AxiosError, AxiosInstance, AxiosResponse, CreateAxiosDefaults } from "axios";
import { toast } from "react-toastify";

type MessageType = "info" | "success" | "warning" | "error";

export class ApiCall<T> {

    private readonly action: () => Promise<AxiosResponse<T>>;
    private messageType: MessageType;

    constructor(action: () => Promise<AxiosResponse<T>>) {
        this.action = action;
        this.messageType = "error";
    }

    public async invoke(): ApiCallResult<T> {
        try {
            const axiosResponse = await this.action();
            return { success: true, data: axiosResponse.data };
        } catch (error: unknown) {
            if (!(error instanceof AxiosError)) {
                toast("Unknown error", { type: "error" });
                return { success: false, data: null };
            }

            let message = error.message;
            const errorResponse = error.response?.data;
            if (errorResponse) {
                message = errorResponse.Error;
            }
            toast(message, { type: this.messageType });
            return { success: false, data: null };
        }
    }
}

export type ApiCallResult<T> = Promise<{ success: boolean, data: T | null }>;

export class ApiClientBase {

    private readonly axiosInstance: AxiosInstance;

    constructor(token: string | null | undefined) {
        const config: CreateAxiosDefaults = {
            baseURL: process.env.REACT_APP_API_URL,
            headers: {
                "Authorization": `Bearer ${token}`,
                get: { "Accept": "application/json" },
                post: { "Content-Type": "application/json" },
                put: { "Content-Type": "application/json" }
            }
        }
        this.axiosInstance = axios.create(config);
    }

    protected call<T>(action: (axios: AxiosInstance) => Promise<AxiosResponse<T>>): ApiCall<T> {
        return new ApiCall<T>(() => action(this.axiosInstance));
    }

    protected get<T>(url: string): ApiCall<T> {
        return this.call(api => api.get(url));
    }

    protected post<T>(url: string, body: unknown): ApiCall<T> {
        return this.call(api => api.post(url, body));
    }

    protected put<T>(url: string, body: unknown): ApiCall<T> {
        return this.call(api => api.put(url, body));
    }

    protected delete<T>(url: string): ApiCall<T> {
        return this.call(api => api.delete(url));
    }
}