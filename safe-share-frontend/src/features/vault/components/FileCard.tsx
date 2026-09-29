import {FileArchive, FileAudio, FileImage, FileQuestion, FileText, FileVideo} from "lucide-react";
import {useState, type DragEvent} from "react";
import type {FileMetadata} from "../../../types/vault.ts";

type FileTone = {
    icon: typeof FileText;
    cardClassName: string;
};

const defaultTone: FileTone = {
    icon: FileQuestion,
    cardClassName: "bg-slate-500",
};

const fileTones: Record<string, FileTone> = {
    "application/pdf": {
        icon: FileText,
        cardClassName: "bg-red-500",
    },
    "application/zip": {
        icon: FileArchive,
        cardClassName: "bg-amber-500",
    },
    "application/x-7z-compressed": {
        icon: FileArchive,
        cardClassName: "bg-amber-500",
    },
    "audio/": {
        icon: FileAudio,
        cardClassName: "bg-violet-500",
    },
    "image/": {
        icon: FileImage,
        cardClassName: "bg-emerald-500",
    },
    "text/": {
        icon: FileText,
        cardClassName: "bg-sky-500",
    },
    "video/": {
        icon: FileVideo,
        cardClassName: "bg-pink-500",
    },
};

function getFileTone(mimeType: string): FileTone {
    return fileTones[mimeType] ?? fileTones[`${mimeType.split("/")[0]}/`] ?? defaultTone;
}

export default function FileCard({fileMetadata}: {fileMetadata: FileMetadata}) {
    const [isDragging, setIsDragging] = useState(false);
    const tone = getFileTone(fileMetadata.mimeExtension);
    const Icon = tone.icon;

    const handleDragStart = (event: DragEvent<HTMLElement>) => {
        event.dataTransfer.effectAllowed = "copy";
        event.dataTransfer.setData("text/plain", fileMetadata.id);
        setIsDragging(true);
    };

    return (
        <article
            draggable
            onDragStart={handleDragStart}
            onDragEnd={() => setIsDragging(false)}
            aria-label={`Drag ${fileMetadata.name}`}
            className={`flex aspect-square cursor-grab items-center justify-center rounded-lg p-3 text-white shadow-lg transition-all active:cursor-grabbing ${
                isDragging
                    ? "scale-[0.98] opacity-60"
                    : `${tone.cardClassName} hover:brightness-110`
            }`}
        >
            <Icon className="h-2/5 w-2/5" strokeWidth={1.5} aria-hidden="true" />
        </article>
    );
}
