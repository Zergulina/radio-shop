import Rating from "../../components/Rating/Rating";
import Tag from "../../components/Tag/Tag";
import { useCart } from "../../store/CartContext";
import type { Product } from "../../types/api";
import AccentButton from "../../UI/buttons/AccentButton/AccentButton";
import classes from "./ProductCard.module.css"

interface ProductCartProps {
    product: Product
    className?: string
}

const ProductCard = ({ product, className }: ProductCartProps) => {
    const {addItem} = useCart();
    return (
        <div className={`${classes.ProductCard} ${className}`}>
            <div style={{ backgroundImage: `url(api/products/images/${product.imageId})` }} className={classes.Image} />
            <h3 className={classes.ProductName}>{product.name}</h3>
            <div className={classes.Row}>
                <Rating rating={product.rating} />
                <span className={classes.OrderAmount}>Заказы: {product.orderAmount}</span>
            </div>
            <p className={classes.Description}>{product.description}</p>
            <div className={classes.Tags}>
                {product.tags.map(tag => <Tag tag={tag}/>)}
            </div>
            <span className={classes.Price}>{product.price} ₽</span>
            <AccentButton className={classes.CartButton} onClick={() => addItem(product)}>В корзину</AccentButton>
        </div>
    );
};

export default ProductCard;