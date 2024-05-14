import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { store } from "../store/configureStore";

axios.defaults.baseURL = import.meta.env.VITE_API_URL;
axios.defaults.withCredentials = true;

const sleep = () => new Promise(rezolve => setTimeout(rezolve, 400));
const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config => {
    const token = store.getState().account.user?.token;
    if(token) config.headers.Authorization = `Bearer ${token}`;
    return config;
})

axios.interceptors.request.use(async response => {
    if(import.meta.env.MODE === 'development') await sleep();
    return response;
}, (error: AxiosError) => {
    const {data, status} = error.response as AxiosResponse;
    switch (status) {
        case 400:
            if (data.errors) {
                const modelStateErrors: string[] = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modelStateErrors.push(data.errors[key])
                    }
                }
                throw modelStateErrors.flat();
            }
            toast.error(data.title);
            break;
        case 401:
            toast.error(data.title);
            break;
        case 403:
            toast.error("You are not allowed to do that!");
            break;
        case 500:
            //router.navigate('/server-error', { state: { error: data } });
            break;
        default:
            break
    }
    return Promise.reject(error.response);
})

const request = {
    get: (url: string, params?: URLSearchParams) => axios.get(url, { params }).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
    postForm: (url:string, data: FormData) => axios.post(url, data, {
        headers: {'Content-type' : 'multipart/form-data'}
    }).then(responseBody),
    putForm: (url:string, data: FormData) => axios.put(url, data, {
        headers: {'Content-type' : 'multipart/form-data'}
    }).then(responseBody)
}

function createFormData(item: any){
    let formData = new FormData();
    for(const key in item){
        formData.append(key, item[key])
    }
    return formData
}

const Account = {
    login: (values: any) => request.post('Account/login', values),
    register: (values: any) => request.post('Account/register', values),
    currentUser: () => request.get('account/currentUser'),
    fetchAddress: () => request.get('account/savedAddress'),
    checkEmailapi: (values: any) => request.post('account/checkEmailapi', values)
}

const agent = {
    Account
}

export default agent;