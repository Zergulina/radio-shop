import { useState } from 'react';
import Panel from '../../UI/separators/Panel/Panel';
import TextInput from '../../UI/inputs/TextInput/TextInput';
import PasswordInput from '../../UI/inputs/PasswordInput/PasswordInput';
import classes from './AdminLoginPanel.module.css';
import AccentButton from '../../UI/buttons/AccentButton/AccentButton';
import { useAuth } from '../../app/routing/AuthContext';
import { Navigate } from 'react-router-dom';
import { loginApi } from './api/LoginApi';

const AdminLoginPanel = () => {
    const [login, setLogin] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const { setToken, token } = useAuth();

    if (token != null) {
        return <Navigate to="/admin"/>
    }

    const loginHandler = () => {
        loginApi(login, password).then(response => {
            console.log(response.data);
            setToken(response.data.token);
        }).catch(err => console.error(err));
    }

    return (
        <Panel className={classes.AdminLoginPanel}>
            <div>
                <h1>Панель администратора</h1>
                <h2>Вход</h2>
            </div>
            <TextInput value={login} setValue={setLogin} label='Логин' placeholder='Введите логин' className={classes.TextInput} />
            <PasswordInput password={password} setPassword={setPassword} className={classes.TextInput} />
            <AccentButton onClick={loginHandler}>Вход</AccentButton>
        </Panel>
    );
};

export default AdminLoginPanel;