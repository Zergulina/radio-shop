import classes from './AdminHeader.module.css';
import { NavLink } from 'react-router-dom';

const AdminHeader = () => {
    return (
        <>
            <header className={classes.AdminHeader}>
                <div className={classes.LeftNavContainer}>
                    <NavLink className={({ isActive }) => `${classes.NavLink} ${isActive ? classes.ActiveNavLink : ""}`} to="/admin">
                        <div className={classes.NavLinkText}>
                            База данных
                        </div>
                    </NavLink>
                </div>
                <div className={classes.RightNavContainer}>
                    <NavLink className={classes.NavLink} to="/logout">
                        <div className={classes.NavLinkText}>
                            Выйти
                        </div>
                    </NavLink>
                </div>
            </header>
        </>
    );
};

export default AdminHeader;