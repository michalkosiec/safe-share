export default async function client<T>(url: string, options: RequestInit = {}): Promise<T | null> {
    const headers = new Headers(options.headers);

    if (!headers.has("Content-Type") && !(options.body instanceof FormData)) {
        headers.set("Content-Type", "application/json");
    }

    const token = localStorage.getItem("token");
    if (token) {
        headers.set("Authorization", `Bearer ${token}`);
    }

    const response = await fetch(url, {...options, headers});
    if (!response.ok) {
        if (response.status === 401) {
            console.error("Session expired or unauthorized.");
        }

        const errorData = await response.json().catch(() => null);
        throw new Error(errorData?.message || `Network error: ${response.status}`);
    }

    if (response.status === 204) {
        return null;
    }

    return response.json();
}