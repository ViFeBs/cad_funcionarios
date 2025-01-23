import { Link } from 'react-router-dom'
import style from '../../styles/components/Button.module.css';
import linkStyle from '../../styles/components/Link.module.css';

function Button ({color, action, text}) {
    if(action === "submit"){
        return( 
            <button type="submit" className={style[color]}>
                {text}
            </button>
        );
    }else{
        return(
            <button className={style[color]}>
                <Link to={action} className={`${linkStyle[color]}`}>{text}</Link>
            </button>
        );
    }   
}

export default Button;