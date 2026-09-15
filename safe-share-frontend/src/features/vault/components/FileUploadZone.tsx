import {type ChangeEvent, type DragEvent, useState} from "react";
import {UploadCloud} from "lucide-react";
import {useFileUpload} from "../hooks/useFileUpload.ts";

export default function FileUploadZone() {
    const [isDragging, setIsDragging] = useState(false);
    const {uploadFile, isUploading} = useFileUpload();

    const processAndUploadFile = async (file: File) => {
        try {
            await uploadFile(file);
        } catch (error) {
            console.error(error);
        }
    }

    const handleUpload = (e: ChangeEvent<HTMLInputElement>) => {
        e.preventDefault();

        const file = e.target.files?.[0];
        if (file) {
            processAndUploadFile(file).catch(console.error);
        }
        e.target.value = '';
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
                {isUploading ? "Uploading file..." : isDragging ? "Drop your file here..." : "Click to upload or drag and drop"}
            </p>
            <p className="text-sm text-gray-100 mt-1">Any encrypted file up to 500MB</p>
            <input type="file" className="hidden" onChange={handleUpload} disabled={isUploading}/>
        </label>
    );
}