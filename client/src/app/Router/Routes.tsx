import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import Login from "../features/account/Login";
import Register from "../features/account/Register";
import HomePage from "../features/HomePage/HomePage";
import NotFound from "../errors/NotFound";
import Account from "../features/account/Account";
import About from "../features/About/About";
import Contact from "../features/Contact/Contact";

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
                {path: 'not-found', element: <NotFound/>},
                {path: 'account', element: <Account/>},
                {path: 'contact', element: <Contact/>},
                {path: 'about', element: <About/>},
                {path: '*', element: <Navigate replace to={'/not-found'}/>}
            ]
        }
    ]
)