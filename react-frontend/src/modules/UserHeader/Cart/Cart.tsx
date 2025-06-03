import React from 'react';
import { useCart } from '../../../store/CartContext';
import { BsDashCircle, BsPlusCircle, BsTrash3 } from 'react-icons/bs';
import classes from './Cart.module.css'
import AccentButton from '../../../UI/buttons/AccentButton/AccentButton';

const Cart: React.FC = () => {
    const {
        state: { items, total },
        removeItem,
        incrementItem,
        decrementItem,
        clearCart
    } = useCart();

    return (
        <div className={classes.Cart}>
            <h2>Корзина</h2>
            {items.length === 0 ? (
                <p>Корзина пуста</p>
            ) : (
                <>
                    <div className={classes.CartList}>
                        {items.map(item => (
                            <div key={item.id} className={classes.Item}>
                                <div style={{ backgroundImage: `url(api/products/images/${item.imageId})` }} className={classes.Image} />
                                <div>
                                    <div>
                                        <h4>{item.name}</h4>
                                        <p>{item.price.toFixed(2)}₽ x {item.quantity} = {(item.price * item.quantity).toFixed(2)}₽</p>
                                    </div>
                                    <div className={classes.Amount}>
                                        <BsDashCircle onClick={() => decrementItem(item.id)} />
                                        <div className={classes.Quantity}>{item.quantity}</div>
                                        <BsPlusCircle onClick={() => incrementItem(item.id)} />
                                        <BsTrash3 onClick={() => removeItem(item.id)} />
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                    <div>
                        <p>Итого: {total.toFixed(2)}₽</p>
                        <AccentButton onClick={clearCart}>Очистить корзину</AccentButton>
                    </div>
                </>
            )}
        </div>
    );
};

export default Cart;