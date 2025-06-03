import { useState } from "react";
import UserHeader from "../../modules/UserHeader/UserHeader";
import type { Product } from "../../types/api";
import ProductCard from "../../modules/ProductCard/ProductCard";
import classes from './CatalogPage.module.css'
import UserFooter from "../../modules/UserFooter/UserFooter";
import AccentButton from "../../UI/buttons/AccentButton/AccentButton";

const CatalogPage = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [count, setCount] = useState<number>(0);
    const [currentPage, setCurrentPage] = useState<number>(1);

    return (
        <div>
            <UserHeader setProducts={setProducts} products={products} setCount={setCount} setCurrentPage={setCurrentPage} currentPage={currentPage} />
            <div className={classes.Catalog}>
                {
                    products.map(product =>
                        <ProductCard
                            product={product}
                        />)
                }
            </div>
            <div className={classes.ButtonWrapper}>
                {
                    count > products.length ?
                        <AccentButton onClick={() => setCurrentPage(currentPage + 1)}>Показать еще</AccentButton>
                        : <></>
                }
            </div>
            <UserFooter />
        </div>
    );
};

export default CatalogPage;