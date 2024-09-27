import axios, {AxiosInstance} from "axios";
import {LOCAL_NET_API_URL} from "./endoints.ts";
import {useAuthStore} from "../store/store.ts";

export const localNetApi: AxiosInstance = axios.create({
    baseURL: LOCAL_NET_API_URL,
    withCredentials: true
});

localNetApi.interceptors.request.use((request) => {

    const {token} = useAuthStore.getState();

    if (token) {
        request.headers.Authorization = `Bearer ${token}`;
    }

    console.log('[INTERCEPTOR] localNetApi REQUEST');
    console.log(request);

    return request;
});

localNetApi.interceptors.request.use(
    (config) => {
        console.log('[INTERCEPTOR] RESPONSE SUCCESS');
        console.log(config);

        return config;
    },
    async (error) => {
        console.log('[INTERCEPTOR] localNetApi RESPONSE ERROR');
        console.error(error);

        const originalRequest = error.config;

        if(error.response.status == 401 && error.config && !error.config._isRetry) {

            originalRequest._isRetry = true;

            try {
                const {isAuthenticated, refresh} = useAuthStore.getState();

                console.log('STATUS 401, refreshing...');

                await refresh();

                console.log('REFRESHED, AUTHENTICATED: ' + isAuthenticated)

                return localNetApi.request(originalRequest);
            } catch(e) {
                console.log(`Authorization failed with error - ${e}`);
            }
        }

        throw error;
    }
);

export default localNetApi;