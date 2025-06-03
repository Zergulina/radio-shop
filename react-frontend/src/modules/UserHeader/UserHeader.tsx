import classes from './UserHeader.module.css'
import TextInput from "../../UI/inputs/TextInput/TextInput";
import { BsCartFill, BsFilter, BsSearch } from "react-icons/bs";
import { useEffect, useId, useState } from "react";
import Listbox from "../../UI/inputs/Listbox/Listbox";
import type { Product, Tag } from '../../types/api';
import { getAllProducts } from './api/getAllProductsApi';
import { ProductQuery } from '../../types/query';
import TagQuerySelector from './TagQuerySelector/TagQuerySelector';
import { getAllTags } from '../AdminCatalog/api/getAllTagsApi';
import Cart from './Cart/Cart';
import { countProducts } from './api/countProductsApi';

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
    setProducts: (products: Product[]) => void,
    products: Product[],
    setCount: (count: number) => void,
    setCurrentPage: (pageNumber: number)=> void,
    currentPage: number
}

const UserHeader = ({ setProducts, products, setCount, setCurrentPage, currentPage }: UserHeaderProps) => {
    const [filterIsOpen, setFilterIsOpen] = useState<boolean>(false);
    const [cartIsOpen, setCartIsOpen] = useState<boolean>(false);

    const [query, setQuery] = useState<ProductQuery>(new ProductQuery(1, 21, "", false, null, null, null, null, "", null))
    const [prevQuery, setPrevQuery] = useState<ProductQuery>(query);

    const [allTags, setAllTags] = useState<Tag[]>([]);
    const [selectedTags, setSelectedTags] = useState<Tag[]>([]);

    if (currentPage != query.pageNumber) {
        const newQuery = { ...query, pageNumber: currentPage };
        setQuery(newQuery);
        setPrevQuery(newQuery)
        getAllProducts(
            (newProducts: Product[]) => setProducts([...products, ...newProducts]),
            currentPage,
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
    else if (JSON.stringify(query) != JSON.stringify(prevQuery)) {
        setCurrentPage(1);
        setPrevQuery({ ...query, pageNumber: 1 });
        setProducts([]);
        getAllProducts(
            setProducts,
            1,
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
        countProducts(
            setCount,
            query.minPrice,
            query.maxPrice,
            query.minRating,
            query.maxRating,
            query.name,
            query.tag,
        );
    }

    useEffect(() => {
        getAllProducts(setProducts, 1, query.pageSize, query.isDescending ? "true" : "false");
        countProducts(
            setCount,
        );
        getAllTags(setAllTags);
    }, [])

    return (
        <header className={classes.UserHeader}>
            <div className={classes.Wrapper}>
                <h1 className={classes.Title}>ШИМ-ШИМ</h1>
                <div className={classes.SelectebleIcon}>
                    <BsFilter className={`${classes.Icon} ${filterIsOpen && classes.IconSelected}`} onClick={() => setFilterIsOpen(!filterIsOpen)} />
                    <div className={`${classes.FilterPanel}`} style={{ display: filterIsOpen ? "flex" : "none" }}>
                        <TextInput value={query.minPrice ? query.minPrice.toString() : ""} setValue={(value: string) => setQuery({ ...query, minPrice: value != "" ? parseFloat(value) : null })} label='Цена от' className={classes.FilterText} inputClassName={classes.FilterText} />
                        <TextInput value={query.maxPrice ? query.maxPrice.toString() : ""} setValue={(value: string) => setQuery({ ...query, maxPrice: value != "" ? parseFloat(value) : null })} label='Цена до' className={classes.FilterText} inputClassName={classes.FilterText} />
                        <TextInput value={query.minRating ? query.minRating.toString() : ""} setValue={(value: string) => setQuery({ ...query, minRating: value != "" ? parseFloat(value) : null })} label='Рейтинг от' className={classes.FilterText} inputClassName={classes.FilterText} />
                        <TextInput value={query.maxRating ? query.maxRating.toString() : ""} setValue={(value: string) => setQuery({ ...query, maxRating: value != "" ? parseFloat(value) : null })} label='Рейтинг до' className={classes.FilterText} inputClassName={classes.FilterText} />
                        <TagQuerySelector allTags={allTags} selectedTags={selectedTags} setSelectedTags={(tags: Tag[]) => { setSelectedTags(tags); setQuery({ ...query, tag: tags.map(t => t.name).join(" ") }) }} />
                    </div>
                </div>
                <TextInput value={query.name ? query.name : ""} setValue={(value: string) => setQuery({ ...query, name: value })} placeholder="Найдется все!" rightIcon={<BsSearch />} className={classes.Search} inputClassName={classes.SearchInput} />
                <Listbox id={useId()} selectOptions={sortOptions} value={query.sortBy} setValue={(value: string) => setQuery({ ...query, sortBy: value })} className={classes.SortListBox} />
                <Listbox id={useId()} selectOptions={descendingOptions} value={query.isDescending ? "true" : "false"} setValue={(value: string) => setQuery({ ...query, isDescending: value == "true" })} className={classes.IsDescendingListBox} />
                <div className={classes.SelectebleIcon}>
                    <BsCartFill className={`${classes.Icon} ${cartIsOpen && classes.IconSelected}`} onClick={() => setCartIsOpen(!cartIsOpen)} />
                    <div className={`${classes.FilterPanel}`} style={{ display: cartIsOpen ? "flex" : "none" }}>
                        <Cart />
                    </div>
                </div>
            </div>
        </header>
    );
};

export default UserHeader;