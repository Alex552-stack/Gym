import { Navigate, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import Login from "../features/account/Login";
import Register from "../features/account/Register";
import HomePage from "../features/HomePage/HomePage";
import NotFound from "../errors/NotFound";
import Account from "../features/account/Account";
import About from "../features/About/About";
import Contact from "../features/Contact/Contact";
import Logout from "../features/account/Logout";

import CheckQr from "../features/QrCode/CheckQr";
import QrGenerator from "../features/admin/QrGenerator";
import AdminPage from "../features/admin/AdminPage";

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
                {path: 'logout', element: <Logout/>},
                {path: 'home', element: <HomePage/>},
                {path: 'not-found', element: <NotFound/>},
                {path: 'account', element: <Account/>},
                {path: 'contact', element: <Contact/>},
                {path: 'about', element: <About/>},
                {path: 'blog', element: <AdminPage/>},
                {path: 'generate-qr', element: <QrGenerator/>},
                {path: 'check-qr', element: <CheckQr/>},
                {path: '*', element: <Navigate replace to={'/not-found'}/>}
            ]
        }
    ]
)