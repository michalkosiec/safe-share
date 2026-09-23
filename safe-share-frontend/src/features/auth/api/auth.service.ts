import apiClient from "../../../api/apiClient.ts";
import {endpoints} from "../../../api/endpoints.ts";

interface CurrentUserResponse { userId: string, username: string }
interface LoginResponse { username: string, userId: string }

export class AuthService {
    static async me() {
        return apiClient<CurrentUserResponse>(endpoints.auth.me, {method: 'GET'});
    }

    static async login(username: string, password: string) {
        return apiClient<LoginResponse>(endpoints.auth.login, {method: 'POST', body: JSON.stringify({username, password})});
    }

    static async logout() {
        return apiClient<void>(endpoints.auth.logout, {method: 'POST'});
    }

    static async register(username: string, password: string, publicKey: string, encryptedPrivateKey: string) {
        return apiClient<void>(endpoints.auth.register, {method: 'POST', body: JSON.stringify({username, password, publicKey, encryptedPrivateKey})});
    }
}