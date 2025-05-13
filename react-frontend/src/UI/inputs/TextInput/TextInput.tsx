import { useState } from 'react';
import classes from './TextInput.module.css'

interface TextInputProps {
    value: string,
    setValue: (newValue: string) => void,
    label?: string,
    placeholder?: string,
    regex?: RegExp,
    wrongMessage?: string,
    isWrongBlock?: boolean,
    rightIcon?: React.ReactNode,
    isHidden?: boolean,
    className?: string,
    inputClassName?: string
}

const TextInput = ({ value, setValue, label, placeholder, regex, wrongMessage, isWrongBlock, rightIcon, isHidden, className, inputClassName }: TextInputProps) => {
    const [wrongFlag, setWrongFlag] = useState<boolean>(false);
    const [prevIsWrongBlock, setPrevIsWrongBlock] = useState<boolean | undefined>(isWrongBlock);


    if (isWrongBlock != prevIsWrongBlock) {
        setPrevIsWrongBlock(isWrongBlock);
        if (isWrongBlock) {
            setWrongFlag(true);
        }
        else {
            if (!regex) return;
            if (!regex.test(value)) setWrongFlag(true)
            else setWrongFlag(false);
        }
    }

    const handleInputChange = (newValue: string) => {
        setValue(newValue);
        if (!regex || isWrongBlock) return;
        if (!regex.test(newValue)) setWrongFlag(true)
        else setWrongFlag(false);
    }

    return (
        <div className={`${classes.TextInput} ${wrongFlag ? classes.Wrong : ""} ${className}`}>
            <label className={`${classes.Label} ${wrongFlag ? classes.Wrong : ""}`}>{label}</label>
            <input
                type={isHidden ? "password" : "text"}
                value={value}
                onChange={e => handleInputChange(e.target.value)}
                className={`${classes.Input} ${wrongFlag ? classes.Wrong : ""} ${inputClassName}`}
                placeholder={placeholder}
            />
            <p className={`${classes.Wrong} ${classes.WrongMessage}`} style={{ opacity: (wrongFlag ? "100%" : "0") }}>{wrongMessage}</p>
            {<div className={`${classes.RightIconWrapper} ${wrongFlag ? classes.Wrong : ""}`}>{rightIcon}</div>}
        </div>
    );
};

export default TextInput;