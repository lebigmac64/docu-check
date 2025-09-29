import React, { useEffect, useRef, useState } from "react";

type Props = {
    text?: string;
    checked?: number;
    total?: number;
    freezeOnComplete?: boolean;
};

export default function ProgressBar({text = "Checking", total = 3, checked = 0, freezeOnComplete = true }
                                    : Props): React.ReactElement {
    const [progress, setProgress] = useState(0);
    const frozenRef = useRef(false);

    let value = total > 0 ? (checked / total) * 100 : 0;

    useEffect(() => {
        const handler = () => { frozenRef.current = false; setProgress(0); };
        window.addEventListener("ProgressBarReset", handler);

        return () => window.removeEventListener("ProgressBarReset", handler);
    }, []);

    useEffect(() => {
        if (freezeOnComplete && frozenRef.current) return;

        const clamped = Math.max(0, Math.min(100, Math.round(value)));
        setProgress((prev) => {
            const next = clamped;
            if (freezeOnComplete && next >= 100) {
                frozenRef.current = true;
                return 100;
            }
            return next;
        });
    }, [value, freezeOnComplete]);

    return (
        <div className="w-full">
            <div
                role="progressbar"
                aria-valuemin={0}
                aria-valuemax={100}
                aria-valuenow={progress}
                className="relative h-8 w-full rounded-md border border-[#3D4052] overflow-hidden"
                style={{ backgroundColor: "#232431" }}
            >
                <div
                    className="h-full transition-[width] duration-500 ease-out animate-pulse"
                    style={{
                        width: `${progress}%`,
                        background:
                            "linear-gradient(90deg, #21BFC2 0%, #A77CFF 100%)",
                    }}
                />
                <div className="absolute inset-0 flex items-center justify-center font-semibold text-sm text-[#E6E6E6]">
                    {`${text} ${checked} / ${total}`}
                </div>
            </div>
        </div>
    );
}
