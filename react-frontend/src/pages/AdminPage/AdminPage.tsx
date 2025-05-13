import { Navigate, Route, Routes } from 'react-router-dom';
import AdminHeader from '../../modules/AdminHeader/AdminHeader';
import AdminCatalogMenu from '../../modules/AdminMenu/AdminMenu';
import AdminCatalog from '../../modules/AdminCatalog/AdminCatalog';
import classes from './AdminPage.module.css'
import AdminTags from '../../modules/AdminTags/AdminTags';

const AdminPage = () => {
    return (
        <div className={classes.Page}>
            <AdminHeader/>
            <Routes>
                <Route path='/' element={<AdminCatalogMenu />}>
                    <Route index element={<Navigate to="catalog" />} />
                    <Route path='/catalog' element={<AdminCatalog />} />
                    <Route path='/tags' element={<AdminTags/>}/>
                </Route>
            </Routes>
        </div>
    );
};

export default AdminPage;
