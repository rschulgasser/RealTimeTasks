import axios from 'axios';
import React, { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { useAuthContext } from '../AuthContext';

const Logout = () => {
    const history = useHistory();
    const {setUser} = useAuthContext();

    useEffect(() => {
        const doLogout = async () => {
            setUser(null);
            await axios.post('/api/account/logout');
            console.log('loggingout');
        }
        
        doLogout();
        history.push('/');

    }, []);

    return <></>
}

export default Logout;