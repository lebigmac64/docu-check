import React, {useEffect, useRef, useState} from "react";
import ProgressBar from "~/components/progress-bar";

type CheckResult = {
    ResultType: number;
    Type: number;
    CheckedAt: string;
    Note: string;
};

const ResultType = {
    Invalid: 0,
    Valid: 1,
    Error: 5
};

const getDocumentType = (type: number): string => {
    switch (type) {
        case 0: return "Občanský průkaz";
        case 4: return "Pas";
        case 6: return "Zbrojní průkaz";
    }

    return "Neznámý typ dokumentu";
}

const getResultText = (result: CheckResult): string => {
    switch (result.ResultType) {
        case ResultType.Invalid:
            return `Neplatný dokument \n(${getDocumentType(result.Type)})`;
        case ResultType.Valid:
            return `Platný dokument \n(${getDocumentType(result.Type)})`;
        case ResultType.Error:
            return `Chyba při kontrole (${result.Note})`;
        default:
            return "Neznámý výsledek";
    }
}

const url = new URL(`https://localhost:7088/api/documents/check/`);

export default function DocumentForm() : React.ReactElement {
    const [docNumber, setDocNumber] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [checked, setChecked] = useState(0);
    const [checkResults, setCheckResults] = useState([] as CheckResult[]);
    const esRef = useRef<EventSource | null>(null);

    useEffect(() => {
        if (!isSubmitting) return;

        const es = new EventSource(url + docNumber, );
        esRef.current = es;

        es.addEventListener("checkResult", (e: MessageEvent) => {
            setCheckResults((last) => [...last, JSON.parse(e.data)]);
            console.log(e);
            setChecked((count) => count + 1);
        });

        es.addEventListener("done", (e :MessageEvent) => {
            es.close();
        });

        return () => es.close();
    }, [isSubmitting]);

    const handleSubmit =
        async (e: React.MouseEvent<HTMLButtonElement>) : Promise<void> =>
    {
        e.preventDefault();
        setIsSubmitting(true);
    }

    return (
        <div className="min-h-screen flex items-center justify-center bg-[#2B2D3A] text-[#E6E6E6] font-sans">
            <div className="bg-[#313445] p-8 rounded-xl w-full max-w-md shadow-2xl border border-[#3D4052]">
                <h1 className="text-xl font-semibold mb-6 text-center">
                    Invalid Document Checker
                </h1>
                <form
                    className="flex flex-col gap-6"
                    onSubmit={(e) => e.preventDefault()}
                >
                    <div className="flex flex-col gap-2">
                        <label htmlFor="doc" className="text-sm text-[#B6BAC5]">
                            Document number
                        </label>
                        <input
                            id="doc"
                            className="bg-[#2A2C39] border border-[#3D4052] text-[#E6E6E6] px-4 py-3 rounded-lg
                            focus:border-[#21BFC2] focus:ring-2 focus:ring-[#21BFC2]/30 outline-none transition"
                            placeholder="e.g., 123456789 or AB123456"
                            value={docNumber}
                            onChange={(e) => setDocNumber(e.target.value)}
                        />
                    </div>
                    {!isSubmitting &&
                    <button
                        disabled={!docNumber.trim()}
                        className="bg-gradient-to-r from-[#21BFC2] to-[#A77CFF] text-[#0E1015] font-semibold py-3
                        rounded-lg shadow-md disabled:opacity-60 disabled:cursor-not-allowed transition"
                        onClick={handleSubmit}
                    >
                        Check now
                    </button>}
                    {isSubmitting && <ProgressBar checked={checked} />}
                </form>
                <ul id="close-history">
                    {checkResults.map((result, index) => (
                        <li key={index} className="mt-4 p-4 bg-[#2A2C39] border border-[#3D4052] rounded-lg">
                            <div className="flex justify-between items-center">
                                <span className="font-semibold">
                                    {getResultText(result)}
                                </span>
                                <span className="text-sm text-[#8D90A0]">
                                    {new Date(result.CheckedAt).toLocaleString(undefined, {
                                        year: "numeric",
                                        month: "2-digit",
                                        day: "2-digit",
                                        hour: "2-digit",
                                        minute: "2-digit"
                                    })}
                                </span>
                            </div>
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
}
