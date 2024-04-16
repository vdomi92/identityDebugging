import { useState, useEffect } from 'react'; 
import { Outlet, Navigate } from 'react-router-dom';

function ProtectedRoutes() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [loading, setLoading] = useState(true);

    const checkCredentials = async () => {

    }

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            setLoading(false);
            setIsAuthenticated(true);
        }
    }, []);


    if (!isAuthenticated) {
        return <Navigate to="/login" />;
    }

    return <Outlet />;
}