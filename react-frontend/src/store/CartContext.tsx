import React, { createContext, useContext, useReducer, useEffect, type ReactNode } from 'react';
import type { Product } from '../types/api';

// Типы для элемента корзины
interface CartItem extends Product {
  quantity: number;
}

// Типы для состояния корзины
interface CartState {
  items: CartItem[];
  total: number;
}

// Типы для действий
type CartAction =
  | { type: 'ADD_ITEM'; payload: Product }
  | { type: 'REMOVE_ITEM'; payload: { id: number } }
  | { type: 'INCREMENT_ITEM'; payload: { id: number } }
  | { type: 'DECREMENT_ITEM'; payload: { id: number } }
  | { type: 'CLEAR_CART' }
  | { type: 'RESTORE_CART'; payload: CartState };

interface CartContextType {
  state: CartState;
  addItem: (product: Product) => void;
  removeItem: (id: number) => void;
  incrementItem: (id: number) => void;
  decrementItem: (id: number) => void;
  clearCart: () => void;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

const initialState: CartState = {
  items: [],
  total: 0,
};

const calculateTotal = (items: CartItem[]): number => {
  return items.reduce((sum, item) => sum + item.price * item.quantity, 0);
};

const cartReducer = (state: CartState, action: CartAction): CartState => {
  let newItems = [...state.items];
  
  switch (action.type) {
    case 'ADD_ITEM':
      const existingItem = newItems.find(item => item.id === action.payload.id);
      if (existingItem) {
        existingItem.quantity += 1;
      } else {
        newItems.push({ ...action.payload, quantity: 1 });
      }
      return { items: newItems, total: calculateTotal(newItems) };
      
    case 'REMOVE_ITEM':
      newItems = newItems.filter(item => item.id !== action.payload.id);
      return { items: newItems, total: calculateTotal(newItems) };
      
    case 'INCREMENT_ITEM':
      newItems = newItems.map(item => 
        item.id === action.payload.id 
          ? { ...item, quantity: item.quantity + 1 } 
          : item
      );
      return { items: newItems, total: calculateTotal(newItems) };
      
    case 'DECREMENT_ITEM':
      newItems = newItems.map(item => 
        item.id === action.payload.id 
          ? { ...item, quantity: Math.max(1, item.quantity - 1) } 
          : item
      );
      return { items: newItems, total: calculateTotal(newItems) };
      
    case 'CLEAR_CART':
      return initialState;
    
    case 'RESTORE_CART':
      return action.payload;
      
    default:
      return state;
  }
};

interface CartProviderProps {
  children: ReactNode;
}

export const CartProvider: React.FC<CartProviderProps> = ({ children }) => {
  const [state, dispatch] = useReducer(cartReducer, initialState);

  useEffect(() => {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
      try {
        const parsedCart = JSON.parse(savedCart);
        dispatch({ type: 'RESTORE_CART', payload: parsedCart });
      } catch (e) {
        console.error('Failed to parse cart data', e);
      }
    }
  }, []);

  useEffect(() => {
    localStorage.setItem('cart', JSON.stringify(state));
  }, [state]);

  const addItem = (product: Product) => {
    dispatch({ type: 'ADD_ITEM', payload: product });
  };

  const removeItem = (id: number) => {
    dispatch({ type: 'REMOVE_ITEM', payload: { id } });
  };

  const incrementItem = (id: number) => {
    dispatch({ type: 'INCREMENT_ITEM', payload: { id } });
  };

  const decrementItem = (id: number) => {
    dispatch({ type: 'DECREMENT_ITEM', payload: { id } });
  };

  const clearCart = () => {
    dispatch({ type: 'CLEAR_CART' });
  };

  const value = {
    state,
    addItem,
    removeItem,
    incrementItem,
    decrementItem,
    clearCart,
  };

  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
};

export const useCart = () => {
  const context = useContext(CartContext);
  if (context === undefined) {
    throw new Error('useCart must be used within a CartProvider');
  }
  return context;
};