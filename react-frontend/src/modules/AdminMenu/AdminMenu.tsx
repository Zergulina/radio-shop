import classes from './AdminMenu.module.css'
import { NavLink, Outlet } from 'react-router-dom';

const AdminCatalogMenu = () => {

    return (
        <div className={classes.Content}>
            <div className={classes.AdminCatalogMenu}>
                <div className={classes.AdminCatalogNavigationContainer}>
                    <NavLink className={({ isActive }) => `${classes.NavLink} ${isActive ? classes.ActiveNavLink : ""}`} to="/admin/catalog">
                        <div className={classes.NavLinkText}>
                            Каталог
                        </div>
                    </NavLink>
                    <NavLink className={({ isActive }) => `${classes.NavLink} ${isActive ? classes.ActiveNavLink : ""}`} to="/admin/tags">
                        <div className={classes.NavLinkText}>
                            Теги
                        </div>
                    </NavLink>
                </div>
            </div>
            <Outlet />
        </div>
    );
};

export default AdminCatalogMenu;