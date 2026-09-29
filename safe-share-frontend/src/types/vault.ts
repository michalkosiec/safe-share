export type FileStatus = "Pending" | "Available" | "Failed";

export interface FileMetadata {
    id: string;
    name: string;
    size: number;
    mimeExtension: string;
    status: FileStatus;
    createdAt: string | Date;
}
