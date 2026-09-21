export default async function apiClient<T>(url: string, options: RequestInit = {}): Promise<T | null> {
    const headers = new Headers(options.headers);

    if (!headers.has("Content-Type") && !(options.body instanceof FormData)) {
        headers.set("Content-Type", "application/json");
    }

    const fetchOptions: RequestInit = {
        ...options,
        headers,
        credentials: "include",
    }
    const response = await fetch(url, fetchOptions);
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