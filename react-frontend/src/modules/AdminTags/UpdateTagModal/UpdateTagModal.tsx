import React, { useState } from 'react';
import type { Tag } from '../../../types/api';
import Modal from '../../../components/Modal/Modal';
import classes from './UpdateTagModal.module.css';
import TextInput from '../../../UI/inputs/TextInput/TextInput';
import AccentButton from '../../../UI/buttons/AccentButton/AccentButton';
import { updateTag } from '../api/updateTagApi';

interface UpdateTagModalProps {
    id: number | string | null,
    tags: Tag[],
    setTags: (tags: Tag[]) => void,
    isShown: boolean,
    closeCallback: () => void
}

const UpdateTagModal: React.FC<UpdateTagModalProps> = ({ id, tags, setTags, isShown, closeCallback }) => {
    const [name, setName] = useState<string>("");
    const [prevIsShown, setPrevIsShown] = useState<boolean>(isShown);

    if (prevIsShown != isShown) {
        setPrevIsShown(isShown);
        if (!isShown) return;
        setName((tags.find(tag => tag.id == id) as Tag).name);
    }

    return (
        <Modal
            isShown={isShown}
            closeCallback={() => {
                closeCallback();
                setName("");
            }}
            className={classes.Modal}
        >
            <div className={classes.Wrapper}>
                <div className={classes.InputsWrapper}>
                    <TextInput value={name} setValue={(name: string) => setName(name)} label='Название' placeholder='Название' className={classes.InputText} />
                </div>
            </div>
            <div className={classes.ButtonWrapper}>
                <AccentButton onClick={() => updateTag((tag: Tag) => setTags([...tags].map(t => tag.id == t.id ? tag : t)), id, { name: name })}>Изменить</AccentButton>
            </div>
        </Modal>
    );
};

export default UpdateTagModal;