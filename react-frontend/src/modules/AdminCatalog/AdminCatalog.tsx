import { useEffect, useState } from 'react';
import type { Product } from '../../types/api';
import { ProductQuery } from '../../types/query';
import { countProducts } from './api/countProductsApi';
import { getAllProducts } from './api/getAllProductsApi';
import Table from '../../components/Table/Table';
import useModal from '../../components/Modal/hooks';
import classes from './AdminCatalog.module.css'
import { deleteProduct } from './api/deleteProductApi';
import CreateNewProductModal from './CreateNewProductModal/CreateNewProductModal';
import UpdateProductModal from './UpdateProductModal/UpdateProductModal';

const AdminCatalog = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [totalSize, setTotalSize] = useState<number>(0);
    const [prevTotalSize, setPrevTotalSize] = useState(totalSize);
    const [rowIndexToEdit, setRowIndexToEdit] = useState<number | null>(null);
    const [query, setQuery] = useState<ProductQuery>(new ProductQuery(
        1,
        25,
        "",
        false,
        null,
        null,
        null,
        null,
        null,
        null
    ));
    const [prevQuery, setPrevQuery] = useState(query);

    const [createNewModalIsShown, createNewModalIsShownToggle] = useModal();
    const [updateModalIsShown, updateModalIsShownToggle] = useModal();
    const [filterModalIsShown, filterModalIsShownToggle] = useModal();

    rowIndexToEdit;
    updateModalIsShown;
    filterModalIsShown;
    useEffect(() => {
        countProducts(setTotalSize);
    }, []);

    const countProductHandler = (count: number) => {
        getAllProducts((products : Product[]) => {
            const newTotalSize = count;
            setTotalSize(newTotalSize);
            setPrevTotalSize(newTotalSize);
            setProducts(products); 
            const maxPageNumber = Math.ceil(newTotalSize / query.pageSize);
            const newQuery = {...query};
            if (query.pageNumber > 1 && query.pageNumber > maxPageNumber) {
                newQuery.pageNumber = maxPageNumber;
            }
            setQuery(newQuery);
        }, query.pageNumber, query.pageSize, query.isDescending ? "true": "false", query.minPrice, query.maxPrice, query.minRating, query.maxRating, query.name, query.tag, query.sortBy)
    };

    if (JSON.stringify(query) != JSON.stringify(prevQuery) || totalSize != prevTotalSize) {
        setPrevQuery(query);
        setPrevTotalSize(totalSize);
        if (
            query.minPrice != prevQuery.minPrice ||
            query.maxPrice != prevQuery.maxPrice ||
            query.minRating != prevQuery.minRating ||
            query.maxRating != prevQuery.maxRating ||
            query.name != prevQuery.name ||
            query.tag != prevQuery.tag
        ) {
            countProducts(countProductHandler, query.minPrice, query.maxPrice, query.minRating, query.maxRating, query.name, query.tag);
        }
        else {
            getAllProducts(setProducts, query.pageNumber, query.pageSize, query.isDescending ? "true" : "false", query.minPrice, query.maxPrice, query.minRating, query.maxRating, query.name, query.tag, query.sortBy);
        }
    };

    const sortOptions = [
        {
            name: "Цена",
            value: "price",
        },
        {
            name: "Рейтинг",
            value: "rating"
        },
        {
            name: "Название",
            value: "name"
        }
    ]

    const headers = [
        "Название",
        "Описание",
        "Цена",
        "Рейтинг",
        "Теги",
        "Id изображения"
    ]

    return (
        <div className={classes.Page}>
             <Table
                headers={headers}
                data={products}
                createCallback={createNewModalIsShownToggle}
                deleteCallback={deleteProduct}
                editCallback={(index) => {
                    setRowIndexToEdit(index);
                    updateModalIsShownToggle();
                }}
                setTotalSize={setTotalSize}
                totalSize={totalSize}
                query={query}
                setQuery={setQuery}
                filterCallback={filterModalIsShownToggle}
                sortOptions={sortOptions}
                imageFlag={true}
                className={classes.Table}
            />
            <CreateNewProductModal
                products={products}
                setProducts={setProducts}
                pageSize={query.pageSize}
                totalSize={totalSize}
                setTotalSize={setTotalSize}
                isShown={createNewModalIsShown}
                closeCallback={createNewModalIsShownToggle}
            />
            <UpdateProductModal
                products={products}
                setProducts={setProducts}
                index={rowIndexToEdit as number}
                isShown={updateModalIsShown}
                closeCallback={updateModalIsShownToggle}
            />
        </div>
    );
};

export default AdminCatalog;