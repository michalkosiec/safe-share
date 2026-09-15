import {Outlet} from "react-router-dom";
// import {SmokeBackground} from "../components/SmokeBackground.tsx";

export default function MainLayout() {
    return (
        <div className="h-screen flex justify-center items-center bg-gray-500">
            {/*<div className="absolute inset-0 z-0">*/}
            {/*    <SmokeBackground smokeColor="#3b82f6" />*/}
            {/*</div>*/}
            <Outlet />
        </div>
        );
}