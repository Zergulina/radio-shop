import React from 'react';
import type { Tag } from '../../../types/api';
import classes from './TagQuerySelector.module.css'

interface TagQuerySelectorProps {
    allTags: Tag[],
    selectedTags: Tag[],
    setSelectedTags: (tags: Tag[]) => void
}

const TagQuerySelector: React.FC<TagQuerySelectorProps> = ({ allTags, selectedTags, setSelectedTags }: TagQuerySelectorProps) => {
    const handleTagClick = (tag: Tag) => {
        if (selectedTags.includes(tag)) {
            setSelectedTags(selectedTags.filter((t) => t.id !== tag.id));
        } else {
            setSelectedTags([...selectedTags, tag]);
        }
    };

    const getTagClassName = (tag: Tag) => {
        let className = classes.Tag;
        if (selectedTags.includes(tag)) {
            className += ` ${classes.TagSelected}`
        }

        return className;
    };

    return (
        <div className={classes.TagSelector}>
            <h3>Теги товара</h3>
            <div className={classes.TagList}>
                {allTags.map((tag) => (
                    <button
                        key={tag.id}
                        className={getTagClassName(tag)}
                        onClick={() => handleTagClick(tag)}
                    >
                        {tag.name}
                    </button>
                ))}
            </div>
        </div>
    );
};

export default TagQuerySelector;