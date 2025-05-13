import React from 'react';
import Modal from '../../Modal/Modal';
import useModal from '../../Modal/hooks';
import AccentButton from '../../../UI/buttons/AccentButton/AccentButton';
import classes from "./ArrayModal.module.css"

interface ArrayModalProps {
    array: Array<any>
}

const ArrayModal: React.FC<ArrayModalProps> = ({ array }) => {
    const [arrayModalIsShown, arrayModalIsShownToggle] = useModal();

    return (
        <>
            <AccentButton onClick={arrayModalIsShownToggle} className={classes.Button}>Просмотреть</AccentButton>
            <Modal isShown={arrayModalIsShown} closeCallback={arrayModalIsShownToggle} className={classes.Modal}>
                {
                    array.map((value: unknown) => {
                        const renderValue = (val: unknown): React.ReactNode => {
                            if (val === null || val === undefined) {
                                return '';
                            } else if (typeof val === 'string' || typeof val === 'number' || typeof val === 'boolean') {
                                return val;
                            } else if (React.isValidElement(val)) {
                                return val;
                            } else if (typeof val === 'object') {
                                if (val instanceof Date) {
                                    return val.toLocaleString();
                                }
                                return (
                                    <div className={classes.Item}>
                                        {Object.entries(val).map(([key, objValue]) => (
                                            <div key={key}>
                                                {key}: {renderValue(objValue)}
                                            </div>
                                        ))}
                                    </div>
                                );
                            }
                            return String(val);
                        };

                        return (
                            <div className={classes.ItemList}>
                                {renderValue(value)}
                            </div>
                        );
                    })
                }
            </Modal>
        </>
    );
};

export default ArrayModal;