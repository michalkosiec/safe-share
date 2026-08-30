export class AuthService {
    private static API_URL = import.meta.env.VITE_API_URL;
    static async login (userName: string, password: string) {
        const request = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({userName, password}),
        }

        const response = await fetch(`${this.API_URL}/auth/login`, request);

        if (!response.ok)
            throw new Error("Failed to login");


        return response.json();
    }

    static async logout (user: string) {
        const request = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({user})
        };

        const response = await fetch(`${this.API_URL}/auth/logout`, request);
        if (!response.ok)
            throw new Error("Failed to logout");
    }

    static async register (userName: string, password: string, publicKey: string, encryptedPrivateKey: string) {
        const request = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({userName, password, publicKey, encryptedPrivateKey}),
        };

        const response = await fetch(`${this.API_URL}/auth/register`, request);
        if (!response.ok)
            throw new Error("Failed to register");
    }
}