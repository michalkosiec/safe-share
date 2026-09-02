import {Outlet} from "react-router-dom";

export default function MainLayout() {
    return (
        <div className="bg-gray-600 h-screen">
            <Outlet />
        </div>
        );
}