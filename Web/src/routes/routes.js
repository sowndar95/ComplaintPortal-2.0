import { Navigate, useRoutes } from 'react-router-dom';
import DashboardLayout from '../layouts/dashboard';
import BlogPage from '../pages/BlogPage';
import UserPage from '../pages/UserPage';
import LoginPage from '../pages/LoginPage';
import ProductsPage from '../pages/ProductsPage';
import DashboardAppPage from '../pages/DashboardAppPage';
import Page404 from '../pages/Page404';

export default function Router() {
    const isAuthenticated = !!localStorage.getItem('userContext');
    const routes = useRoutes([
        {
            path: '',
            element: <Navigate to='/login' />,
        },
        {
            path: '/dashboard',
            element: isAuthenticated ? <DashboardLayout /> : <Navigate to='/login'/>,
            children: [
                { element: <Navigate to="/dashboard/app" />, index: true },
                { path: 'app', element: <DashboardAppPage /> },
                { path: 'user', element: <UserPage /> },
                { path: 'products', element: <ProductsPage /> },
                { path: 'blog', element: <BlogPage /> },
                { path: 'error', element: <Page404 /> },
            ],
        },
        {
            path: 'login',
            element: isAuthenticated ? <Navigate to="/dashboard/app" /> : <LoginPage />,
        },
        {
            path: '*',
            element: <Navigate to="/dashboard/error" replace />,
        },
    ]);

    return routes;
}
