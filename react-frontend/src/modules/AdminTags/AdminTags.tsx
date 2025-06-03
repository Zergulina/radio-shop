import { useEffect, useId, useMemo, useState } from 'react';
import classes from './Admin.module.css';
import type { Tag } from '../../types/api';
import { BsPencilSquare, BsX } from 'react-icons/bs';
import { getAllTags } from './api/getAllTagsApi';
import { deleteTag } from './api/deleteTagApi';
import AccentButton from '../../UI/buttons/AccentButton/AccentButton';
import useModal from '../../components/Modal/hooks';
import CreateNewTagModal from './CreateNewTagModal/CreateNewTagModal';
import UpdateTagModal from './UpdateTagModal/UpdateTagModal';
import Listbox from '../../UI/inputs/Listbox/Listbox';
import TextInput from '../../UI/inputs/TextInput/TextInput';

const AdminTags = () => {
    const [tags, setTags] = useState<Tag[]>([]);
    const [deletingId, setDeletingId] = useState<string | number | null>(null);
    const [prevDeletingId, setPrevDeletingId] = useState<string | number | null>(deletingId);

    const [updatingId, setUpdatingId] = useState<string | number | null>(null);

    const [createNewModalIsShown, createNewModalIsShownToggle] = useModal();
    const [updateModalIsShown, updateModalIsShownToggle] = useModal();

    const [sortBy, setSortBy] = useState<string>("");
    const [isDescending, setIsDescending] = useState<boolean>(false);
    const [name, setName] = useState<string>("");

    useEffect(() => {
        getAllTags(setTags);
    }, []);

    if (deletingId != prevDeletingId) {
        setPrevDeletingId(deletingId);
        deleteTag(() => { setTags([...tags].filter(tag => tag.id != deletingId)) }, deletingId);
    }

    const sortOptions = [
        {
            name: "Id",
            value: "id",
        },
        {
            name: "Название",
            value: "name"
        }
    ]

    const filteredAndSortedProducts = useMemo(() => {
        let result = [...tags];

        result = result.filter(tag =>
            tag.name.toLowerCase().includes(name.toLowerCase())
        );

        if (sortBy == "id") {
            result.sort((a, b) =>
                isDescending ? b.id - a.id : a.id - b.id
            )
        }
        else if (sortBy == "name") {
            result.sort((a, b) =>
                isDescending ? b.name.localeCompare(a.name) : a.name.localeCompare(b.name)
            )
        }

        return result;
    }, [tags, sortBy, name, isDescending]);


    return (
        <>
            <div className={classes.Page}>
                <div className={classes.TablePlate}>
                    <div className={classes.TableController}>
                        <div className={classes.TableControllerWrapper}>
                            <label className={classes.SortParameters}>
                                Сортировка
                                <Listbox id={useId()} selectOptions={[{ value: "", name: "Без сортировки" }, ...sortOptions]} setValue={(value: string) => setSortBy(value)} value={sortBy} />
                                <Listbox id={useId()} selectOptions={[{ value: "false", name: "По возрастанию" }, { value: "true", name: "По убыванию" }]} setValue={(value: string) => setIsDescending(value == "true")} />
                            </label>
                            <TextInput value={name} setValue={setName} placeholder='Название' label='Сортировка по названию' className={classes.TextInput} />
                        </div>
                    </div>
                    <div className={classes.TableWrapper}>
                        <table className={classes.Table}>
                            <thead>
                                <tr>
                                    <th>
                                    </th>
                                    <th>
                                        <div className={classes.HeaderCellContent}>
                                            Id
                                        </div>
                                    </th>
                                    <th>
                                        <div className={classes.HeaderCellContent}>
                                            Название
                                        </div>
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    filteredAndSortedProducts.map((tag, index) => <tr id={tag.id.toString()}>
                                        <td className={classes.FirstColumn}>
                                            <div className={classes.FirstColumnCellColumn}>
                                                <div className={classes.FirstColumnCellColumnText}>
                                                    {index + 1}
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div className={classes.CellText}>
                                                {tag.id}
                                            </div>
                                        </td>
                                        <td>
                                            <div className={classes.CellText}>
                                                {tag.name}
                                            </div>
                                        </td>
                                        <td className={classes.RowControlsWrapper}>
                                            <div className={classes.RowControls}>
                                                <div className={classes.IconWrapper}>
                                                    <BsPencilSquare className={classes.Icon} onClick={() => {
                                                        setUpdatingId(tag.id)
                                                        updateModalIsShownToggle();
                                                    }} />
                                                </div>
                                                <div className={classes.IconWrapper}>
                                                    <BsX className={classes.Icon} onClick={() => {
                                                        setDeletingId(tag.id);
                                                    }} />
                                                </div>
                                            </div>
                                        </td>
                                    </tr>)
                                }
                            </tbody>
                        </table>
                    </div>
                </div>
                <div className={classes.TableControllers}>
                    <div className={classes.TableControllersLeft}>
                        <AccentButton onClick={createNewModalIsShownToggle} className={classes.Button}> Добавить </AccentButton>
                    </div>
                </div>
            </div>
            <CreateNewTagModal
                tags={tags}
                setTags={setTags}
                isShown={createNewModalIsShown}
                closeCallback={createNewModalIsShownToggle}
            />
            <UpdateTagModal
                tags={tags}
                setTags={setTags}
                isShown={updateModalIsShown}
                closeCallback={updateModalIsShownToggle}
                id={updatingId}
            />
        </>
    )
};

export default AdminTags;