import {type ChangeEvent, type DragEvent, useState} from "react";
import {UploadCloud} from "lucide-react";

export default function FileUploadZone() {
    const [uploading, setUploading] = useState(false);
    const [isDragging, setIsDragging] = useState(false);

    const processAndUploadFile = async (file: File) => {
        setUploading(true);
        console.log("Process and upload file: ", file);
        setUploading(false);
    }
    const handleUpload = (e: ChangeEvent<HTMLInputElement>) => {
        e.preventDefault();
        setUploading(true);
        setUploading(false);
    }

    const handleDragOver = (e: DragEvent<HTMLLabelElement>) => {
        e.preventDefault();
        setIsDragging(true);
    }

    const handleDragLeave = (e: DragEvent<HTMLLabelElement>) => {
        e.preventDefault();
        setIsDragging(false);
    }

    const handleDrop = (e: DragEvent<HTMLLabelElement>) => {
        e.preventDefault();
        setIsDragging(false);
        const file = e.dataTransfer?.files[0];
        if (file)
            processAndUploadFile(file).catch(console.error);
    }

    return (
        <label onDragOver={handleDragOver} onDragLeave={handleDragLeave} onDrop={handleDrop} className={`flex flex-col items-center justify-center h-64 border-2 border-dashed rounded-xl cursor-pointer transition-all ${
            isDragging
                ? "border-blue-400 bg-blue-500/10 scale-[1.01]"
                : "border-white/20 hover:border-blue-500 hover:bg-white/5"
        }`}>
            <UploadCloud className={`w-11 h-11 mb-3 ${isDragging ? "text-blue-400 scale-125" : "text-blue-400 animate-bounce"}`}/>
            <p className="text-lg font-medium text-white">
                {uploading ? "Uploading file..." : isDragging ? "Drop your file here..." : "Click to upload or drag and drop"}
            </p>
            <p className="text-sm text-gray-100 mt-1">Any encrypted file up to 500MB</p>
            <input type="file" className="hidden" onChange={handleUpload} disabled={uploading}/>
        </label>
    );
}