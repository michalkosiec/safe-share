const API_URL: string = import.meta.env.VITE_API_URL;

export const endpoints = {
    auth: {
        me: `${API_URL}/auth/me`,
        login: `${API_URL}/auth/login`,
        register: `${API_URL}/auth/register`,
        logout: `${API_URL}/auth/logout`,
    },
    files: {
        downloadUrl: `${API_URL}/files/download-url`,
        uploadUrl: `${API_URL}/files/upload-url`,
    }
}