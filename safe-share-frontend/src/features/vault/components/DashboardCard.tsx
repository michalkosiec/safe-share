import type {ReactNode} from "react";

export default function DashboardCard({children, className=""}: {children: ReactNode, className?: string}) {
    return (
        <section className={`${className} bg-black/85 rounded-2xl border border-white/10 p-5 shadow-lg backdrop-blur-sm`}>
            {children}
        </section>
    )
}