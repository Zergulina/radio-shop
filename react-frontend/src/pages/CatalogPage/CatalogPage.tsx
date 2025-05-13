import { useEffect, useState } from "react";
import UserHeader from "../../modules/UserHeader/UserHeader";
import type { Product } from "../../types/api";
import ProductCard from "../../modules/ProductCard/ProductCard";
import classes from './CatalogPage.module.css'

const CatalogPage = () => {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {

    }, [])

    return (
        <div>
            <UserHeader setProducts={setProducts} />
            <div className={classes.Catalog}>
                {
                    products.map(product => 
                    <ProductCard 
                        product={product}
                    />)
                }
            </div>
        </div>
    );
};

export default CatalogPage;