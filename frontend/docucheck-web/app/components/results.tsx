import {
    type CheckResult, type FullResult,
    getDocumentType, getInvalidDocTypes,
    ResultType
} from "~/components/document-form/document-form.module";
import React from "react";

export default function Results({fullResults}: {fullResults: {CheckResults: CheckResult[], DocumentNumber?: string, CheckedAt: string}[]}) {

    function getResultText(checkResults: CheckResult[]) {
        const invalid = checkResults.find((r) => r.ResultType === ResultType.Invalid);
        if (invalid !== undefined) {
            return (
                <div className="flex items-center gap-3 bg-red-900/30 border-l-4 w-full max-w-md border-red-500 p-3 rounded shadow transition-all duration-300">
                    <div>
                        <p className="text-red-400 font-semibold">Dokument evidován v databázi</p>
                        <p className="text-red-400 font-semibold">Neplatný od: {invalid.RecordedAt}</p>
                        <p className="text-red-200">Typy: <span className="font-bold">{getInvalidDocTypes(checkResults)}</span></p>
                    </div>
                </div>
            );
        }
        return (
            <div className="flex items-center gap-3 bg-green-900/30 border-l-4 border-green-500 p-3 rounded">
                <p className="text-green-300 font-semibold">Nenalezen v databázi</p>
            </div>
        );
    }

    return (
        <div>
            <ul>
                {fullResults.length === 0 && (
                    <li>
                        <p className="text-center text-gray-400 italic">Zatím nejsou k dispozici žádné výsledky.</p>
                    </li>
                )}
                {fullResults.map((result, index) => (
                    <li
                        key={index}
                        className="mb-4 p-6 border min-w-80 border-[#3D4052] rounded-xl bg-gradient-to-br from-[#232431] to-[#1a1b22] shadow-lg"
                    >
                        <div className="flex items-center justify-between mb-2">
                            <h5 className="text-sm font-bold text-[#e0e0e0]">
                                Výsledek kontroly #{fullResults.length - index}
                            </h5>
                            <span className="text-xs text-gray-400">
                                {new Date(result.CheckedAt).toLocaleString("cs-CZ")}
                            </span>
                        </div>
                        <h5 className="text-sm mb-3">Č. {result.DocumentNumber}</h5>
                        {getResultText(result.CheckResults)}
                    </li>
                ))}
            </ul>
        </div>
    );
}
