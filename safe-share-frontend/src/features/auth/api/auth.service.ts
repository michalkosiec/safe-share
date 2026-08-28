export const AuthService = {
    login: async (user: string, password: string) => {
        const request = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({user, password}),
        }

        const response = await fetch('/api/auth/login', request);

        if (!response.ok) {
            throw new Error("Failed to login");
        }

        return response.json();
    },

    logout: async (user: string) => {
        const request = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({user})
        };

        await fetch('/api/auth/logout', request);
    }
}