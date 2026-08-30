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
            setIsLoading(false);
        };
        initAuth().catch(console.error);
    }, []);

    const login = async (username: string, password: string) => {
        setIsLoading(true);
        try {
            const userData = await AuthService.login(username, password);
            setUser(userData);
        } finally {
            setIsLoading(false);
        }
    }

    const logout = async (username: string) => {
        setIsLoading(true);
        try {
            await AuthService.logout(username);
        } finally {
            setIsLoading(false);
        }
    }

    const register = async (username: string, password: string, publicKey: string, encryptedPrivateKey: string) => {
        setIsLoading(true);
        try {
            await AuthService.register(username, password, publicKey, encryptedPrivateKey);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <AuthContext.Provider value={{user, isAuthenticated: !!user, isLoading, login, logout, register}}>
            {children}
        </AuthContext.Provider>
    )
}