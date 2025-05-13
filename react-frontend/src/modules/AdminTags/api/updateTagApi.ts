import axios from "axios";
import type { Tag, UpdateTag } from "../../../types/api";

export const updateTag = (updateTagCallback: (tag: Tag) => void, id: number | string | null, tag: UpdateTag) => {
    axios.put(`/api/tags/${id}`, tag).then(res => updateTagCallback(res.data));
}