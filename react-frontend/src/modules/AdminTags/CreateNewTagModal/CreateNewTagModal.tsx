import React, { useState } from 'react';
import type { Tag } from '../../../types/api';
import Modal from '../../../components/Modal/Modal';
import classes from './CreateNewTagModal.module.css'
import { createTag } from '../api/createTagApi';
import AccentButton from '../../../UI/buttons/AccentButton/AccentButton';
import TextInput from '../../../UI/inputs/TextInput/TextInput';

interface CreateNewTagModalProps {
    tags: Tag[],
    setTags: (tags: Tag[]) => void,
    isShown: boolean,
    closeCallback: () => void
}

const CreateNewTagModal: React.FC<CreateNewTagModalProps> = ({ tags, setTags, isShown, closeCallback }) => {
    const [name, setName] = useState<string>("");

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
                <AccentButton onClick={() => createTag((tag: Tag) => setTags([...tags, tag]), { name: name })}>Создать</AccentButton>
            </div>
        </Modal>
    );
};

export default CreateNewTagModal;