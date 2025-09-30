import { type CheckResult } from "~/components/index/document-form/document-form.module";
import React from "react";
import ResultNode from "~/components/index/result-node/result-node";

export default function ResultList({
  fullResults,
}: {
  fullResults: {
    CheckResults: CheckResult[];
    DocumentNumber?: string;
    CheckedAt: string;
  }[];
}) {
  return (
      <ul>
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
            <ResultNode checkResults={result.CheckResults} />
          </li>
        ))}
      </ul>
  );
}
