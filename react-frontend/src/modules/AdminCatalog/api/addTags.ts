import axios from "axios"

export const addTags = (addTagsCallback: () => void, productId: number, tagIds: number[]) => {
    axios.put(`/api/products/${productId}/tags`, {tagIds}).then(() => addTagsCallback());
}