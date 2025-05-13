import axios from "axios";
import type { Tag } from "../../../types/api";

export const getAllTags = (setTags: (tags: Tag[]) => void) => {
    axios.get<Tag[]>("/api/tags").then(res => setTags(res.data));
}