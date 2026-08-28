import type {ReactNode} from "react";

interface Props {
    title: string;
    children: ReactNode;
}
export default function AuthCard({title, children}: Props) {
    return (
        <div className="flex flex-col w-2/5 p-6 bg-[rgba(0,0,0,0.6)] rounded-xl backdrop-blur-xl border border-black/10 shadow-2xl text-white max-w-2xl">
            <div>
                <h1 className="text-3xl font-bold tracking-wider">{title}</h1>
            </div>
            {children}
        </div>
    )
}