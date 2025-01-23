import {createBrowserRouter, RouterProvider} from "react-router-dom";

//PAGES
import Login from '../pages/Login';
import Home from '../pages/Home'

export default function Rotas(){
    const router = createBrowserRouter([
        {
            path: "/",
            element: <Login />,
        },
        {
            path: "/Home",
            element: <Home />,
        },
    ]);
    
    return(
        <RouterProvider router={router} />
    )
}
