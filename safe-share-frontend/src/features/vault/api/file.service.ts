import client from "../../../api/client.ts";
import {endpoints} from "../../../api/endpoints.ts";
import rawClient from "../../../api/rawClient.ts";

export class FileService {
    static async requestUploadUrl(fileName: string, contentType: string): Promise<string> {
        const response = await client<{ url: string }>(endpoints.files.uploadUrl, {method: 'POST', body: JSON.stringify({fileName, contentType})})
        if (!response) {
            throw new Error("Cannot fetch upload url.");
        }

        return response.url;
    }

    static async requestDownloadUrl(fileId: string): Promise<string> {
        const response = await client<{ url: string }>(endpoints.files.downloadUrl(fileId), {method: 'GET', body: JSON.stringify({fileId})})
        if (!response) {
            throw new Error("Cannot fetch download url.");
        }

        return response.url;
    }

    static async uploadToS3(url: string, file: File): Promise<void> {
        await rawClient(url, {
            method: 'PUT',
            headers: {
                "Content-Type": file.type || "application/octet-stream",
            },
            body: file,
            credentials: 'omit'
        })
    }

    static async downloadFromS3(url: string, fileName: string): Promise<File> {
        const response = await rawClient(url, {method: 'POST', credentials: 'omit'});
        const blob = await response.blob();
        const contentType = response.headers.get("Content-Type") || "application/octet-stream";

        return new File([blob], fileName, { type: contentType });
    }
}