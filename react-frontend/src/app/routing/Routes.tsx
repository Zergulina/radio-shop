import { RouterProvider, createBrowserRouter } from "react-router-dom";
import LoginPage from "../../pages/LoginPage/LoginPage";
import { useRoleBasedView } from "./useRoleBasedView";
import LogoutPage from "../../pages/LogoutPage/LogoutPage";

const Routes = () => {
    const routes = [
        {
            path: "/",
            element: useRoleBasedView({
                "admin": <div>Admin</div>
            }, <LoginPage />),
        },
        {
            path: "logout",
            element: <LogoutPage/>
        }
    ];

    const router = createBrowserRouter([
        ...routes
    ]);

    return <RouterProvider router={router} />;
};

export default Routes;  