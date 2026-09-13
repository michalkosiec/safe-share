// Raw client does not return parsed result
export default async function rawClient(url: string, options: RequestInit = {}): Promise<Response> {
    const response = await fetch(url, options);
    if (!response.ok) {
        const errorText= await response.text().catch(()=> "");
        let errorMessage = `HTTP Error ${response.status}`;
        try {
            const errorJson = JSON.parse(errorText);
            errorMessage = errorJson.message || errorMessage;
        } catch {
            errorMessage = errorText || errorMessage;
        }

        throw new Error(errorMessage);
    }

    return response;
}