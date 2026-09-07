import client from "../../../api/client.ts";
import {endpoints} from "../../../api/endpoints.ts";

interface CurrentUserResponse { userId: string, username: string }
interface LoginResponse { token: string, username: string, userId: string }

export class AuthService {
    static async me() {
        return client<CurrentUserResponse>(endpoints.auth.me, {method: "GET"});
    }
    static async login(username: string, password: string) {
        return client<LoginResponse>(endpoints.auth.login, {method: 'POST', body: JSON.stringify({username, password})});
    }

    static async logout(user: string) {
        return client<void>(endpoints.auth.logout, {method: 'POST', body: JSON.stringify({user})});
    }

    static async register(username: string, password: string, publicKey: string, encryptedPrivateKey: string) {
        return client<void>(endpoints.auth.register, {method: 'POST', body: JSON.stringify({username, password, publicKey, encryptedPrivateKey})});
    }
}