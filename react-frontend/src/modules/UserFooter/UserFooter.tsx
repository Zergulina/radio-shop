import classes from './UserFooter.module.css' 

const UserFooter = () => {
    return (
        <footer className={classes.footer}>
            <div className={classes.contactInfo}>
                <h3>Контактная информация</h3>
                <p>Email: antonchernyaevt@gmail.com</p>
                <p>Телефон: +7 (928) 117-88-51</p>
                <p>Адрес: г. Ростов-на-Дону, ул. Гагарина, д. 1</p>
            </div>
            <div className={classes.author}>
                <p>© 2025 Автор проекта: Чеярнев Антон</p>
            </div>
        </footer>
    );
};

export default UserFooter;