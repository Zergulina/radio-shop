import axios from "axios"

export const deleteTag = (deleteCallback: () => void, id: string | number | null) => {
    axios.delete(`/api/tags/${id}`).then(() => deleteCallback());
}