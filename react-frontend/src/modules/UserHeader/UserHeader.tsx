import classes from './UserHeader.module.css'
import TextInput from "../../UI/inputs/TextInput/TextInput";
import { BsCartFill, BsFilter, BsPersonFill, BsSearch } from "react-icons/bs";
import { useEffect, useId, useState } from "react";
import Listbox from "../../UI/inputs/Listbox/Listbox";
import type { Product, Tag } from '../../types/api';
import { getAllProducts } from './api/getAllProductsApi';
import { ProductQuery } from '../../types/query';
import TagQuerySelector from './TagQuerySelector/TagQuerySelector';
import { getAllTags } from '../AdminCatalog/api/getAllTagsApi';

const sortOptions = [
    {
        value: "",
        name: "По умолчанию"
    },
    {
        value: "price",
        name: "Цена"
    },
    {
        value: "rating",
        name: "Рейтинг"
    },
    {
        value: "name",
        name: "Название"
    }
]

const descendingOptions = [
    {
        value: "false",
        name: "По возрастанию"
    },
    {
        value: "true",
        name: "По убыванию"
    }
]

interface UserHeaderProps {
    setProducts: (products: Product[]) => void
}

const UserHeader = ({ setProducts }: UserHeaderProps) => {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const [query, setQuery] = useState<ProductQuery>(new ProductQuery(1, 50, "", false, null, null, null, null, "", null))
    const [prevQuery, setPrevQuery] = useState<ProductQuery>(query);

    const [allTags, setAllTags] = useState<Tag[]>([]);
    const [selectedTags, setSelectedTags] = useState<Tag[]>([]);

    const getRandomInt = (min: number, max: number): number => {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    const getRandomFloatRounded = (min: number, max: number, decimals: number): number => {
        const rand = Math.random() * (max - min) + min;
        return parseFloat(rand.toFixed(decimals));
    }

    if (query != prevQuery) {
        setPrevQuery(query);
        getAllProducts(
            (products : Product[]) => {products.forEach(product => {product.orderAmount = getRandomInt(300, 3000); product.rating = getRandomFloatRounded(2, 5.1, 1);}); setProducts(products);},
            query.pageNumber,
            query.pageSize,
            query.isDescending ? "true" : "false",
            query.minPrice,
            query.maxPrice,
            query.minRating,
            query.maxRating,
            query.name,
            query.tag,
            query.sortBy
        )
    }

    useEffect(() => {
        getAllProducts((products : Product[]) => {products.forEach(product => {product.orderAmount = getRandomInt(300, 3000); product.rating = getRandomFloatRounded(2, 5.1, 1);}); setProducts(products);}, query.pageNumber, query.pageSize, query.isDescending ? "true" : "false");
        getAllTags(setAllTags);
    }, [])

    return (
        <div className={classes.UserHeader}>
            <div className={classes.Wrapper}>
                <h1 className={classes.Title}>ШИМ-ШИМ</h1>
                <div className={classes.FilterIcon}>
                    <BsFilter className={`${classes.Icon} ${isOpen && classes.IconSelected}`} onClick={() => setIsOpen(!isOpen)} />
                    {
                        <div className={`${classes.FilterPanel}`} style={{ opacity: isOpen ? "100%" : 0 }}>
                            <TextInput value={query.minPrice ? query.minPrice.toString() : ""} setValue={(value: string) => setQuery({ ...query, minPrice: value != "" ? parseFloat(value) : null })} label='Цена от' className={classes.FilterText} inputClassName={classes.FilterText} />
                            <TextInput value={query.maxPrice ? query.maxPrice.toString() : ""} setValue={(value: string) => setQuery({ ...query, maxPrice: value != "" ? parseFloat(value) : null })} label='Цена до' className={classes.FilterText} inputClassName={classes.FilterText} />
                            <TextInput value={query.minRating ? query.minRating.toString() : ""} setValue={(value: string) => setQuery({ ...query, minRating: value != "" ? parseFloat(value) : null })} label='Рейтинг от' className={classes.FilterText} inputClassName={classes.FilterText} />
                            <TextInput value={query.maxRating ? query.maxRating.toString() : ""} setValue={(value: string) => setQuery({ ...query, maxRating: value != "" ? parseFloat(value) : null })} label='Рейтинг до' className={classes.FilterText} inputClassName={classes.FilterText} />
                            <TagQuerySelector allTags={allTags} selectedTags={selectedTags} setSelectedTags={(tags: Tag[]) => { setSelectedTags(tags); setQuery({ ...query, tag: tags.map(t => t.name).join(" ") }) }} />
                        </div>
                    }
                </div>
                <TextInput value={query.name ? query.name : ""} setValue={(value: string) => setQuery({ ...query, name: value })} placeholder="Найдется все!" rightIcon={<BsSearch />} className={classes.Search} inputClassName={classes.SearchInput} />
                <Listbox id={useId()} selectOptions={sortOptions} value={query.sortBy} setValue={(value: string) => setQuery({ ...query, sortBy: value })} className={classes.SortListBox} />
                <Listbox id={useId()} selectOptions={descendingOptions} value={query.isDescending ? "true" : "false"} setValue={(value: string) => setQuery({ ...query, isDescending: value == "true" })} className={classes.IsDescendingListBox} />
                <BsCartFill className={classes.Icon} />
                <BsPersonFill className={classes.Icon} />
            </div>
        </div>
    );
};

export default UserHeader;