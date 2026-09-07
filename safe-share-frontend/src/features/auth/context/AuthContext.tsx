import {createContext, type ReactNode, useEffect, useState} from "react";
import {AuthService} from "../api/auth.service.ts";

interface User {
    id: string;
    name: string;
}

interface AuthContextType {
    user: User | null;
    isAuthenticated: boolean;
    isLoading: boolean;
    login: (username: string, password: string) => Promise<void>;
    logout: (username: string) => Promise<void>;
    register: (username: string, password: string, publicKey: string, encryptedPrivateKey: string) => Promise<void>;
}

/* eslint-disable react-refresh/only-export-components */
export const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: {children: ReactNode}) => {
    const [user, setUser] = useState<User | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        const initAuth = async () => {
            const token = localStorage.getItem("token");
            if (token) {
                try {
                    const response = await AuthService.me();
                    if (response) {
                        setUser({id: response.userId, name: response.username});
                    }
                } catch {
                    localStorage.removeItem("token");
                }
            }
            setIsLoading(false);
        };
        initAuth().catch(console.error);
    }, []);

    const login = async (username: string, password: string) => {
        const response = await AuthService.login(username, password);
        if (response?.token) {
            localStorage.setItem('token', response.token);
            setUser({id: response.userId, name: response.username});
        }

    }

    const logout = async (username: string) => {
        await AuthService.logout(username);
        setUser(null);
        localStorage.removeItem('token');
    }

    const register = async (username: string, password: string, publicKey: string, encryptedPrivateKey: string) => {
        await AuthService.register(username, password, publicKey, encryptedPrivateKey);
    }

    return (
        <AuthContext.Provider value={{user, isAuthenticated: !!user, isLoading, login, logout, register}}>
            {children}
        </AuthContext.Provider>
    )
}