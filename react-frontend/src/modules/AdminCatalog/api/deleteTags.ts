import axios from "axios"

export const deleteTags = (deleteTagsCallback : () => void, productId: number, tagIds: number[]) => {
    axios.delete(`/api/products/${productId}/tags`, {data: {tagIds}}).then(() => deleteTagsCallback());
}