import { BsStarFill } from 'react-icons/bs';
import classes from './Rating.module.css'

interface RatingProps {
    rating: number,
    className?: string
}

const Rating = ({rating, className} : RatingProps) => {
    return (
        <div className={`${className}`}>
            <BsStarFill className={classes.RatingStar}/> <span className={classes.RatingText}>{rating.toFixed(1)}</span>
        </div>
    );
};

export default Rating;