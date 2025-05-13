import type { Tag as T } from '../../types/api';
import classes from './Tag.module.css'

interface TagProps {
    tag: T
    className?: string
}

const Tag = ({tag, className}: TagProps) => {
    return (
        <div className={`${classes.Tag} ${className}`}>
            {tag.name}
        </div>
    );
};

export default Tag;