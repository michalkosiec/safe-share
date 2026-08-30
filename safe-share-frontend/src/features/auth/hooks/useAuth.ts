import {useContext} from "react";
import {AuthContext} from "../context/AuthContext.tsx";

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context)
        throw new Error("useAuth must be used within AuthProvider");

    return context;
}

export const useUser = () => {
    const { user, isAuthenticated, isLoading } = useAuth();
    return { user, isAuthenticated, isLoading };
}

export const useAuthActions = () => {
    const { login, logout } = useAuth();
    return { login, logout };
}