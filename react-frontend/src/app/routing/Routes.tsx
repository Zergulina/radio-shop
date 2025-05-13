import { RouterProvider, createBrowserRouter } from "react-router-dom";
import CatalogPage from "../../pages/CatalogPage/CatalogPage";
import { useRoleBasedView } from "./useRoleBasedView";
import AdminLoginPage from "../../pages/AdminLoginPage/AdminLoginPage";
import LogoutPage from "../../pages/LogoutPage/LogoutPage";
import AdminPage from "../../pages/AdminPage/AdminPage";

const Routes = () => {
    const routes = [
        {
            path: "/",
            element: <CatalogPage />,
        },
        {
            path: "/admin/*",
            element: useRoleBasedView({
                Admin: <AdminPage/>
            }, <AdminLoginPage/>)
        },
        {
            path: "/logout",
            element: <LogoutPage/>
        }
    ];

    const router = createBrowserRouter([
        ...routes
    ]);

    return <RouterProvider router={router} />;
};

export default Routes;  