import React, { useEffect, useRef, useState } from 'react';
import type { Tag, Product } from '../../../types/api';
import Modal from '../../../components/Modal/Modal';
import classes from './UpdateProductModal.module.css'
import TextInput from '../../../UI/inputs/TextInput/TextInput';
import AccentButton from '../../../UI/buttons/AccentButton/AccentButton';
import { updateProduct } from '../api/updateProductApi';
import TagSelector from '../TagsSelector/TagsSelector';
import { getAllTags } from '../api/getAllTagsApi';
import { addTags } from '../api/addTags';
import { deleteTags } from '../api/deleteTags';

interface UpdateProductModalProps {
    products: Product[],
    index: number,
    setProducts: (products: Product[]) => void,
    isShown: boolean,
    closeCallback: () => void
}

interface Data {
    name: string,
    description: string,
    price: string,
    imageId?: string
}

const UpdateProductModal: React.FC<UpdateProductModalProps> = ({ products, index, setProducts, isShown, closeCallback }) => {
    const [data, setData] = useState<Data>({
        name: "",
        description: "",
        price: "",
        imageId: undefined
    })
    const [prevIsShown, setPrevIsShown] = useState<boolean>(isShown);
    const [image, setImage] = useState<File | null>(null);

    const [allTags, setAllTags] = useState<Tag[]>([]);

    const [tagsToAdd, setTagsToAdd] = useState<Tag[]>([]);
    const [tagsToDelete, setTagsToDelete] = useState<Tag[]>([]);

    const imageRef = useRef<HTMLInputElement>(null);

    useEffect(() => {
        getAllTags(setAllTags);
    }, []);

    if (prevIsShown != isShown) {
        setPrevIsShown(isShown);
        if (!isShown) return;
        setData({
            name: products[index].name,
            description: products[index].description,
            price: products[index].price.toString(),
            imageId: products[index].imageId
        });
    }

    const getImageUrl = () => {
        if (image) {
            return URL.createObjectURL(image);
        }
        return undefined;
    }

    const updateProductCallback = (product: Product) => {
        const newProducts = [...products];
        newProducts[index] = product;
        setProducts(newProducts);
    }

    const addTagsCallback = () => {
        const newProducts = [...products];
        newProducts[index].tags = newProducts[index].tags.concat(tagsToAdd)
        setProducts(newProducts);
        setTagsToAdd([]);
    }

    const deleteTagsCallback = () => {
        const newProducts = [...products];
        newProducts[index].tags = newProducts[index].tags.filter(t => !tagsToDelete.map(x => x.id).includes(t.id))
        setProducts(newProducts);
        setTagsToDelete([]);
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
                    imageId: ""
                });
            }}
            className={classes.Modal}
        >
            <div className={classes.Wrapper}>
                <div className={classes.ImageWrapper}>
                    <div style={{ backgroundImage: image ? `url(${getImageUrl()})` : `url(/api/products/images/${data.imageId})` }} className={classes.Image} />
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
                                setImage(e.target.files[0]);
                            }
                        }} />
                </div>
            </div>
            <TagSelector allTags={allTags} productTags={products[index] ? products[index].tags : []} setSelectedNewTags={setTagsToAdd} selectedNewTags={tagsToAdd} setSelectedDeleteTags={setTagsToDelete} selectedDeleteTags={tagsToDelete}/>
            <div className={classes.ButtonWrapper}>
                <AccentButton onClick={() => {
                    if (!products[index]) return;
                    updateProduct(updateProductCallback, products[index].id, { name: data.name, description: data.description, price: data.price != "" ? Number.parseFloat(data.price) : 0 });
                    addTags(addTagsCallback, products[index].id, tagsToAdd.map(tag => tag.id));
                    deleteTags(deleteTagsCallback, products[index].id, tagsToDelete.map(tag => tag.id));
                }}>Изменить</AccentButton>
            </div>
        </Modal>
    );
};

export default UpdateProductModal;