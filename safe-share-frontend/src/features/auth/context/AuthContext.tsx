import {createContext} from "react";

interface AuthContextType {
    user: any | null;
    isAuthenticated: boolean;
    isLoading: boolean;
    login: (username: string, password: string) => Promise<void>;
    logout: (username: string) => Promise<void>;
}

export const AuthContext = createContext<AuthContextType | null>(null)