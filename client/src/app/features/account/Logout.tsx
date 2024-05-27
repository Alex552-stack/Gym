import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { signOut } from './accountSlice';
import { toast } from 'react-toastify';


const LogoutPage: React.FC = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(signOut());
        navigate('/');
        toast.success("Succesfully logged out");
    }, [dispatch, navigate]);

    return (
        <div>
            Logging out...
        </div>
    );
};

export default LogoutPage;
