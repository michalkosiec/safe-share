import {FolderLock, Settings, Shield} from "lucide-react";
import {Link} from "react-router-dom";

export default function Sidebar() {
    return (
        <aside className="bg-black/85 rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm flex flex-col">
            <div className="flex items-center gap-3 px-2 mb-8 mt-2">
                <Shield className="w-8 h-8 text-white" />
                <span className="text-xl font-bold text-white tracking-wider">SafeShare</span>
            </div>
            <nav className="flex-1 space-y-2">
                <Link to="/vault" className="flex flex-row">
                    <FolderLock className="w-6 h-6 text-white" />
                    <span className="text-white ml-3">Your Safe</span>
                </Link>
                <Link to="/settings" className="flex flex-row">
                    <Settings className="w-6 h-6 text-white" />
                    <span className="text-white ml-3">Settings</span>
                </Link>
            </nav>
        </aside>
    )
}