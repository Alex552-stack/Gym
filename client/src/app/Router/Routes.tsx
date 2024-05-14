import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import Login from "../features/account/Login";
import Register from "../features/account/Register";
import HomePage from "../features/HomePage/HomePage";

export const router = createBrowserRouter(
    [
        {
            path:'/',
            element: <App/>,
            children: [
                /*{
                    element: <RequireAuth />, children: [
                        { path: 'checkout', element: <CheckoutWrapper /> },
                        { path: 'orders', element: <Orders /> },
                    ]
                },*/

                {path: 'login', element: <Login/>},
                {path: 'register', element: <Register/>},
                {path: 'home', element: <HomePage/>},
                {path: '*', element: <Navigate replace to={'/not-found'}/>}
            ]
        }
    ]
)