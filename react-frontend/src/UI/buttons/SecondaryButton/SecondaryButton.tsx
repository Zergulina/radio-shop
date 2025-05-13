import React from 'react';
import BaseButton from '../BaseButton/BaseButton';
import classes from './SecondaryButton.module.css'

interface SecondatyButtonProps {
    children?: React.ReactNode,
    icon?: React.ReactNode,
    className?: string,
}

const SecondaryButton = ({ children, icon, className }: SecondatyButtonProps) => {
    return (
        <BaseButton icon={icon} className={`${classes.SecondaryButton} ${className}`}>{children}</BaseButton>
    );
};

export default SecondaryButton;