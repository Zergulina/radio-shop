import React from 'react';
import classes from './Panel.module.css'

interface PanelProps {
    children: React.ReactNode,
    className?: string
}

const Panel = ({children, className}: PanelProps) => {
    return (
        <div className={`${classes.Panel} ${className}`}>
            {children}
        </div>
    );
};

export default Panel;