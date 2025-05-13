import { useEffect } from 'react';
import { useAuth } from '../../app/routing/AuthContext';
import { Navigate } from 'react-router-dom';

const LogoutPage = () => {
    const { token, setToken } = useAuth();

    const handleLogout = () => {
        setToken(null);
    };

    if (token == null) {
        return <Navigate to="/"/>
    }

    useEffect(() =>{
        handleLogout();
    },[])

    return (
        <div>
            Logout page
        </div>
    );
};

export default LogoutPage;