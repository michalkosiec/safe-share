import { Folder, Settings, Shield, User, Users } from "lucide-react";
import { Link } from "react-router-dom";

export default function Sidebar() {
    return (
        <aside className="bg-black/85 rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm flex flex-col h-full">
            <div className="flex items-center gap-3 px-2 mb-6 mt-2">
                <span className="text-xl font-bold text-white tracking-wider">SafeShare</span>
            </div>

            <hr className="border-t-3 border-white/20 mb-12" />

            <nav className="flex-1 space-y-3">
                <Link to="/vault" className="flex items-center p-3 rounded-xl hover:bg-white/10 transition-all duration-200">
                    <Folder className="w-6 h-6 text-white" />
                    <span className="text-white ml-3 text-lg">Your Safe</span>
                </Link>

                <Link to="/share" className="flex items-center p-3 rounded-xl hover:bg-white/10 transition-all duration-200">
                    <Users className="w-6 h-6 text-white" />
                    <span className="text-white ml-3 text-lg">Sharing</span>
                </Link>

                <Link to="/security" className="flex items-center p-3 rounded-xl hover:bg-white/10 transition-all duration-200">
                    <Shield className="w-6 h-6 text-white" />
                    <span className="text-white ml-3 text-lg">Security</span>
                </Link>
            </nav>

            <nav className="flex items-center gap-5 mb-2 px-3">
                <Link to="/settings" className="hover:opacity-75 transition-all duration-200 hover:rotate-45">
                    <Settings className="w-7 h-7 text-white" />
                </Link>
                <Link to="/user" className="hover:opacity-75 transition-all duration-200 hover:scale-110">
                    <User className="w-7 h-7 text-white" />
                </Link>
            </nav>
        </aside>
    )
}