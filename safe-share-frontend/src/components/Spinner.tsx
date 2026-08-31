interface SpinnerProps {
    className?: string;
    fullScreen?: boolean;
    darkOverlay?: boolean;
}

export function Spinner({ className = "w-12 h-12 text-gray-400", fullScreen = false, darkOverlay = false }: SpinnerProps) {
    const spinnerContent = (
        <svg
            className={className}
            viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg"
        >
            <style>
                {`
                    .spinner_blade { animation: spinner_fade 1s infinite linear; }
                    @keyframes spinner_fade { 
                        0% { opacity: 1; } 
                        100% { opacity: 0; } 
                    }
                `}
            </style>
            {[...Array(12)].map((_, i) => (
                <g transform={`rotate(${i * 30} 12 12)`} key={i}>
                    <line
                        x1="12" y1="2" x2="12" y2="7"
                        stroke="currentColor"
                        strokeWidth="2.5"
                        strokeLinecap="round"
                        className="spinner_blade"
                        style={{ animationDelay: `${(i - 12) * 0.0833}s` }}
                    />
                </g>
            ))}
        </svg>
    );

    if (fullScreen) {
        return (
            <div className={`fixed inset-0 z-50 flex items-center justify-center ${darkOverlay ? "bg-[rgba(0,0,0,0.15)]" : ""} transition-opacity duration-300`}>
                {spinnerContent}
            </div>
        );
    }

    return spinnerContent;
}