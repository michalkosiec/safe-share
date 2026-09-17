import type {ReactNode} from "react";

interface Props {
    title: string;
    children: ReactNode;
}
export default function AuthCard({title, children}: Props) {
    return (
        <div className="flex flex-col w-2/5 text-white max-w-2xl bg-[rgb(28,28,28)] rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm">
            <div>
                <h1 className="text-3xl font-bold tracking-wider">{title}</h1>
            </div>
            {children}
        </div>
    )
}