import React from 'react';
import type { Tag } from '../../../types/api';
import classes from './TagsSelector.module.css';

interface TagSelectorProps {
  allTags: Tag[];
  productTags: Tag[];
  setSelectedNewTags: (selectedTags: Tag[]) => void;
  selectedNewTags: Tag[],
  setSelectedDeleteTags: (selectedTags: Tag[]) => void;
  selectedDeleteTags: Tag[];
}

const TagSelector: React.FC<TagSelectorProps> = ({
  allTags,
  productTags,
  setSelectedNewTags,
  selectedNewTags,
  setSelectedDeleteTags,
  selectedDeleteTags
}) => {
  const isProductTag = (tag: Tag) => {
    return productTags.some((productTag) => productTag.id === tag.id);
  };

  const isSelectedNewTag = (tag: Tag) => {
    return selectedNewTags.some((selectedTag) => selectedTag.id === tag.id);
  };

  const isSelectedDeleteTag = (tag: Tag) => {
    return selectedDeleteTags.some((selectedTag) => selectedTag.id === tag.id);
  }

  const handleTagClick = (tag: Tag) => {
    if (isSelectedDeleteTag(tag)) {
      setSelectedDeleteTags(selectedDeleteTags.filter((t) => t.id !== tag.id));
    } else if (isProductTag(tag)) {
      setSelectedDeleteTags([...selectedDeleteTags, tag]);
    } else if (isSelectedNewTag(tag)) {
      setSelectedNewTags(selectedNewTags.filter((t) => t.id !== tag.id));
    } else {
      setSelectedNewTags([...selectedNewTags, tag]);
    }
  };

  const getTagClassName = (tag: Tag) => {
    let className = classes.Tag;
    if (isSelectedDeleteTag(tag)) {
      className += ` ${classes.TagDeleteSelected}`
    } else if (isProductTag(tag)) {
      className += ` ${classes.TagProduct}`;
    } else if (isSelectedNewTag(tag)) {
      className += ` ${classes.TagSelected}`;
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

export default TagSelector;