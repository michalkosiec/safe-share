import client from "../../../api/client.ts";
import {endpoints} from "../../../api/endpoints.ts";
import rawClient from "../../../api/rawClient.ts";

export class FileService {
    static async requestUploadUrl(fileName: string, contentType: string): Promise<string> {
        const url = await client<string>(endpoints.files.uploadUrl, {method: 'POST', body: JSON.stringify({fileName, contentType})})
        if (!url) {
            throw new Error("Cannot fetch upload url.");
        }

        return url;
    }

    static async requestDownloadUrl(fileId: string): Promise<string> {
        const url = await client<string>(endpoints.files.downloadUrl(fileId), {method: 'POST', body: JSON.stringify({fileId})})
        if (!url) {
            throw new Error("Cannot fetch download url.");
        }

        return url;
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

    // static async downloadFromS3(url: string, fileId: string): Promise<File> {
    //
    // }
}