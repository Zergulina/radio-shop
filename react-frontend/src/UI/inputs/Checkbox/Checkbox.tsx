import classes from './Checkbox.module.css'

interface CheckboxProps {
    id: string,
    value: boolean,
    onChange: () => void,
    className?: string
}
const Checkbox = ({id, value, onChange, className}: CheckboxProps) => {
    return (
        <input type='checkbox' id={id} checked={value} onChange={onChange} className={`${classes.Checkbox} ${className}`} style={{backgroundImage: `url(./icon-check.png)`}}/>
    );
};

export default Checkbox;