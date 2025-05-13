import axios from "axios";
import type { CreateTag, Tag } from "../../../types/api";

export const createTag = (createTagCallback: (tag: Tag) => void, tag: CreateTag) => {
    axios.post<Tag>("/api/tags", tag).then(res => createTagCallback(res.data));
}