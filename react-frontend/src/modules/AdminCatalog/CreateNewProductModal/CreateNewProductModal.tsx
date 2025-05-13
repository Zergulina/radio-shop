import React, { useRef, useState } from 'react';
import type { Product } from '../../../types/api';
import Modal from '../../../components/Modal/Modal';
import classes from './CreateNewProductModal.module.css'
import TextInput from '../../../UI/inputs/TextInput/TextInput';
import AccentButton from '../../../UI/buttons/AccentButton/AccentButton';
import { createProduct } from '../api/createProductApi';

interface CreateNewProductModalProps {
    products: Product[],
    setProducts: (products: Product[]) => void,
    pageSize: number,
    totalSize: number,
    setTotalSize: (totalSize: number) => void,
    isShown: boolean,
    closeCallback: () => void
}

interface Data {
    name: string,
    description: string,
    price: string,
    image: File | null
}

const CreateNewProductModal: React.FC<CreateNewProductModalProps> = ({ products, setProducts, pageSize, totalSize, setTotalSize, isShown, closeCallback }) => {
    const [data, setData] = useState<Data>({
        name: "",
        description: "",
        price: "",
        image: null
    })

    const imageRef = useRef<HTMLInputElement>(null);

    products;
    setProducts;
    pageSize;
    totalSize;
    setTotalSize;

    const createProductCallback = (product: Product) => {
        if (products.length < pageSize) setProducts([...products, product]);
        setTotalSize(totalSize + 1);
    }

    const getImageUrl = () => {
        if (data.image) {
            return URL.createObjectURL(data.image);
        }
        return undefined;
    }

    return (
        <Modal
            isShown={isShown}
            closeCallback={() => {
                closeCallback();
                setData({
                    name: "",
                    description: "",
                    price: "",
                    image: null
                });
            }}
            className={classes.Modal}
        >
            <div className={classes.Wrapper}>
                <div className={classes.ImageWrapper}>
                    <div style={{ backgroundImage: data.image ? `url(${getImageUrl()})` : undefined }} className={classes.Image} />
                </div>
                <div className={classes.InputsWrapper}>
                    <TextInput value={data.name} setValue={(name: string) => setData({ ...data, name: name })} label='Название' placeholder='Название' className={classes.InputText} />
                    <TextInput value={data.description} setValue={(description: string) => setData({ ...data, description })} label='Описание' placeholder='Описание' className={classes.InputText} />
                    <TextInput value={data.price} setValue={(price: string) => setData({ ...data, price })} label='Цена' placeholder='0' className={classes.InputText} />
                    <input
                        type='file'
                        ref={imageRef}
                        onChange={(e) => {
                            if (e.target.files && e.target.files.length > 0) {
                                setData({ ...data, image: e.target.files[0] });
                            }
                        }} />
                </div>
            </div>
            <div className={classes.ButtonWrapper}>
                <AccentButton onClick={() => createProduct(createProductCallback, { name: data.name, description: data.description, price: data.price != "" ? Number.parseFloat(data.price) : 0, imageFile: data.image })}>Создать</AccentButton>
            </div>
        </Modal>
    );
};

export default CreateNewProductModal;