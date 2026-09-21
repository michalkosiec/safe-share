import {useState} from "react";
import {FileService} from "../api/file.service.ts";

export const useFileUpload = () => {
    const [isUploading, setIsUploading] = useState(false);
    const [error, setError] = useState<Error | null>(null);

    const uploadFile = async (file: File): Promise<void> => {
        setIsUploading(true);
        setError(null);

        try {
            const response = await FileService.requestUploadUrl(file.name, file.type || "application/octet-stream");

            await FileService.uploadToS3(response.url, file);
            await FileService.completeFileUpload(response.id);
        } catch (err) {
            const uploadError = err instanceof Error ? err : new Error("Unknown error occurred while uploading file");

            setError(uploadError);
            throw uploadError
        } finally {
            setIsUploading(false);
        }
    }
    return {uploadFile, isUploading, error};
}