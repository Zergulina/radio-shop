import React, { useId, useState } from 'react';
import classes from './Table.module.css';
import { BsFilter, BsPencilSquare, BsX } from 'react-icons/bs';
import AccentButton from '../../UI/buttons/AccentButton/AccentButton';
import Checkbox from '../../UI/inputs/Checkbox/Checkbox';
import RadioButton from '../../UI/inputs/RadioButton/RadioButton';
import SmallArrowButton from '../../UI/buttons/SmallArrowButton/SmallArrowButton';
import Listbox from '../../UI/inputs/Listbox/Listbox';
import type { QueryBase } from '../../types/query';
import ArrayModal from './ArrayModal/ArrayModal';

interface TableProps {
    headers: string[];
    data: any[] | null;
    createCallback: () => void;
    deleteCallback: (id: string | number | null) => Promise<void>;
    editCallback: (index: number, id: string | number) => void;
    filterCallback: () => void;
    totalSize: number;
    setTotalSize: (size: number) => void;
    query: QueryBase;
    setQuery: (query: any) => void;
    sortOptions: { value: string; name: string }[];
    imageFlag?: boolean;
    className?: string;
}

const Table: React.FC<TableProps> = ({
    headers,
    data,
    createCallback,
    deleteCallback,
    editCallback,
    filterCallback,
    totalSize,
    setTotalSize,
    query,
    setQuery,
    sortOptions,
    imageFlag = false,
    className = ''
}) => {
    const [deletingId, setDeletingId] = useState<string | number | null>(null);
    const [prevDeletingId, setPrevDeletingId] = useState<string | number | null>(deletingId);
    const [showIdFlag, setShowIdFlag] = useState<string>("false");
    const tableUniqueName = useId();

    if (deletingId != prevDeletingId) {
        deleteCallback(deletingId).then(() => {
            setPrevDeletingId(deletingId);
            let pageNumberBuff = query.pageNumber;
            const totalSizeBuff = totalSize - 1;
            setTotalSize(totalSizeBuff);
            if (query.pageNumber > 1 && totalSizeBuff / (Math.floor(totalSizeBuff / query.pageSize) * query.pageSize) <= 1) {
                pageNumberBuff -= 1;
                setQuery({ ...query, pageNumber: pageNumberBuff });
            }
        }).catch(e => console.log(e));
    }

    const setPageSizeHandler = (newPageSize: number) => {
        const maxPageNumber = Math.ceil(totalSize / newPageSize);
        const newQuery = { ...query };
        if (query.pageNumber > 1 && query.pageNumber > maxPageNumber) {
            newQuery.pageNumber = maxPageNumber;
        }
        newQuery.pageSize = newPageSize;
        setQuery(newQuery);
    }

    return (
        <div className={className}>
            <div className={classes.TablePlate}>
                <div className={classes.TableController}>
                    <button className={classes.FilterButton} onClick={filterCallback}><BsFilter className={classes.FilterIcon} />Фильтры</button>
                    <div>
                        <label className={classes.SortParameters}>
                            Сортировка
                            <Listbox id={useId()} selectOptions={[{ value: "", name: "Без сортировки" }, ...sortOptions]} setValue={(value: string) => setQuery({ ...query, sortBy: value })} value={query.sortBy} />
                            <Listbox id={useId()} selectOptions={[{ value: "false", name: "По возрастанию" }, { value: "true", name: "По убыванию" }]} setValue={(value: string) => setQuery({ ...query, isDescending: value == "true" })} />
                        </label>
                    </div>
                </div>
                <div className={classes.TableWrapper}>
                    <table className={classes.Table}>
                        <thead>
                            <tr>
                                <th />
                                {
                                    showIdFlag == "true" &&
                                    <th>
                                        <div className={classes.HeaderCellContent}>
                                            Id
                                        </div>
                                    </th>
                                }
                                {
                                    imageFlag &&
                                    <th>
                                        <div className={classes.HeaderCellContent}>
                                            Изображение
                                        </div>
                                    </th>
                                }
                                {
                                    headers.map((header, index) =>
                                        <th key={index}>
                                            <div className={classes.HeaderCellContent}>
                                                {header}
                                            </div>
                                        </th>
                                    )
                                }
                                <th />
                            </tr>
                        </thead>
                        <tbody>
                            {
                                data &&
                                data.map((row, index) =>
                                    <tr key={row.id}>
                                        <td className={classes.FirstColumn}>
                                            <div className={classes.FirstColumnCellColumn}>
                                                <div className={classes.FirstColumnCellColumnText}>
                                                    {(query.pageNumber - 1) * query.pageSize + index + 1}
                                                </div>
                                            </div>
                                        </td>
                                        {
                                            showIdFlag == "true" &&
                                            <td>
                                                <div className={classes.CellText}>
                                                    {row.id}
                                                </div>
                                            </td>
                                        }
                                        {
                                            imageFlag &&
                                            <td>
                                                {
                                                    row.imageId != undefined && (() => { console.log(row.imageId); return true })() &&
                                                    <img src={`/api/products/images/${row.imageId}`} alt="" />
                                                }
                                            </td>
                                        }
                                        {
                                            Object.values(row).slice(1).map((value: unknown, i: number) => {
                                                const renderValue = (val: unknown): React.ReactNode => {
                                                    if (val === null || val === undefined) {
                                                        return '';
                                                    } else if (typeof val === 'string' || typeof val === 'number' || typeof val === 'boolean') {
                                                        return <div className={classes.CellText}>
                                                            {val}
                                                        </div>;
                                                    } else if (React.isValidElement(val)) {
                                                        return val;
                                                    } else if (typeof val === 'object') {
                                                        if (Array.isArray(val)) {
                                                            return <ArrayModal array={val} />
                                                        }
                                                        if (val instanceof Date) {
                                                            return <div className={classes.CellText}>
                                                                {val.toLocaleString()}
                                                            </div>;
                                                        }
                                                        return (
                                                            <>
                                                                {Object.entries(val).map(([key, objValue]) => (
                                                                    <div key={key}>
                                                                        <span className={classes.ObjectKey}>{key}:</span> {renderValue(objValue)}
                                                                    </div>
                                                                ))}
                                                            </>
                                                        );
                                                    }
                                                    return String(val);
                                                };

                                                return (
                                                    <td key={i}>
                                                        {renderValue(value)}
                                                    </td>
                                                );
                                            })
                                        }
                                        <td className={classes.RowControlsWrapper}>
                                            <div className={classes.RowControls}>
                                                <div className={classes.IconWrapper}>
                                                    <BsPencilSquare className={classes.Icon} onClick={() => editCallback(index, row.id)} />
                                                </div>
                                                <div className={classes.IconWrapper}>
                                                    <BsX className={classes.Icon} onClick={() => {
                                                        setDeletingId(row.id);
                                                    }} />
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                )
                            }
                        </tbody>
                    </table>
                </div>
            </div>
            <div className={classes.TableControllers}>
                <div className={classes.TableControllersLeft}>
                    <div className={classes.PageSizeSelection}>
                        <div className={classes.PageSizeSelectionTitle}>
                            <div>
                                Записи
                            </div>
                        </div>
                        <div className={classes.RadioButtons}>
                            <RadioButton name={tableUniqueName + "pageSize"} checked={query.pageSize === 25} id={tableUniqueName + '25'} onChange={() => setPageSizeHandler(25)} label="25" />
                            <RadioButton name={tableUniqueName + "pageSize"} checked={query.pageSize === 50} id={tableUniqueName + '50'} onChange={() => setPageSizeHandler(50)} label="50" />
                            <RadioButton name={tableUniqueName + "pageSize"} checked={query.pageSize === 100} id={tableUniqueName + '100'} onChange={() => setPageSizeHandler(100)} label="100" />
                        </div>
                    </div>
                    <div className={classes.ShowIdSelection}>
                        <div>
                            <label htmlFor={tableUniqueName + 'showId'}>Показывать Id</label>
                            <Checkbox id={tableUniqueName + 'showId'} value={showIdFlag == "true"} onChange={() => {console.log(showIdFlag);setShowIdFlag((showIdFlag == "false") ? "true" : "false")}} />
                        </div>
                    </div>
                    <AccentButton onClick={createCallback} className={classes.Button}> Добавить </AccentButton>
                </div>
                <div className={classes.TableControllersRight}>
                    <div className={classes.PageNumberSelection}>
                        <SmallArrowButton onClick={() => { if (query.pageNumber > 1) setQuery({ ...query, pageNumber: query.pageNumber - 1 }) }} />
                        {
                            query.pageNumber > 3 &&
                            <>
                                <RadioButton name={tableUniqueName + "pageNumber"} checked={false} id={tableUniqueName + '1'} onChange={() => setQuery({ ...query, pageNumber: 1 })} label="1" className={classes.RadioButtons} />
                                <div className={classes.LeftEllipsis}>
                                    ...
                                </div>
                            </>
                        }
                        <div className={`${classes.RadioButtons} ${classes.PageNumberSelectionRadioButtons}`}>
                            {
                                Array.from({
                                    length:
                                        totalSize > 0 ?
                                            query.pageNumber > 3 ?
                                                Math.ceil(totalSize / query.pageSize) - query.pageNumber + 2
                                                : Math.ceil(totalSize / query.pageSize)
                                            : 1
                                }, (_, i) => i + 1 + (query.pageNumber > 3 ? query.pageNumber - 2 : 0)).map(val =>
                                    <RadioButton key={val} name={tableUniqueName + "pageNumber"} checked={val === query.pageNumber} id={tableUniqueName + val} onChange={() => setQuery({ ...query, pageNumber: val })} label={val.toString()} className={classes.RadioButtons} />
                                )
                            }
                        </div>
                        {
                            query.pageNumber < Math.ceil(totalSize / query.pageSize) - 4 &&
                            <>
                                <div className={classes.RightEllipsis}>
                                    ...
                                </div>
                                <RadioButton name={tableUniqueName + "pageNumber"} checked={false} id={tableUniqueName + Math.ceil(totalSize / query.pageSize)} label={Math.ceil(totalSize / query.pageSize).toString()} onChange={() => setQuery({ ...query, pageNumber: Math.ceil(totalSize / query.pageSize) })} className={classes.RadioButtons} />
                            </>
                        }
                        <SmallArrowButton onClick={() => { if (query.pageNumber < Math.ceil(totalSize / query.pageSize)) setQuery({ ...query, pageNumber: query.pageNumber + 1 }) }} className={classes.RightArrow} />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Table;