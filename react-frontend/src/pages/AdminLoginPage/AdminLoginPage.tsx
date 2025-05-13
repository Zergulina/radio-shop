import AdminLoginPanel from '../../modules/AdminLoginPanel/AdminLoginPanel';
import classes from './AdminLoginPage.module.css'

const AdminLoginPage = () => {
    return (
        <div className={classes.Page}>
            <AdminLoginPanel/>
        </div>
    );
};

export default AdminLoginPage;