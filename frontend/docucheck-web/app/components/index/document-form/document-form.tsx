import React, { useEffect, useRef, useState } from "react";
import ProgressBar from "~/components/index/progress-bar/progress-bar";
import {
  type CheckResult,
  type FullResult,
  getInvalidDocTypes,
  ResultType,
} from "~/components/index/document-form/document-form.module";
import ResultList from "~/components/index/result-list/result-list";
import {API_ROOT} from "~/config";

export default function DocumentForm(): React.ReactElement {
  const [docNumber, setDocNumber] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [currentResults, setCurrentResults] = useState([] as CheckResult[]);
  const [results, setResults] = useState([] as FullResult[]);
  const [total, setTotal] = useState(0);
  const esRef = useRef<EventSource | null>(null);
  const currentResultsRef = useRef<CheckResult[]>([]);

  useEffect(() => {
    currentResultsRef.current = currentResults;
  }, [currentResults]);

  useEffect(() => {
    if (!isSubmitting) return;

    const url = new URL(
      `${API_ROOT}api/documents/check/${docNumber}`,
    );
    const es = new EventSource(url);
    esRef.current = es;

    es.addEventListener("total", (e: MessageEvent) => {
      const parsed = JSON.parse(e.data) as { total: number };
      setTotal(parsed.total);
    });

    es.addEventListener("checkResult", (e: MessageEvent) => {
      const parsed = JSON.parse(e.data) as CheckResult;
      setCurrentResults((last) => [parsed, ...last]);
    });

    es.addEventListener("done", (e: MessageEvent) => {
      const fullResult = {
          DocumentNumber: docNumber,
          CheckResults: currentResultsRef.current,
          CheckedAt: new Date().toISOString(),
      } as FullResult;

      es.close();
      setIsSubmitting(false);
      setResults((last) => [fullResult, ...last]);
      resetForm();
    });

    return () => {
      es.close();
    };
  }, [isSubmitting]);

  const handleSubmit = async (
    e: React.MouseEvent<HTMLButtonElement>,
  ): Promise<void> => {
    e.preventDefault();
    setIsSubmitting(true);
  };

  function resetForm() {
    setDocNumber("");
    window.dispatchEvent(new CustomEvent("ProgressBarReset"));
    setCurrentResults([] as CheckResult[]);
  }

  function handleCancel(e: React.MouseEvent<HTMLButtonElement>) {
    e.preventDefault();
    setIsSubmitting(false);
    esRef.current?.close();
    resetForm();
  }

  return (
      <>
    <div className="flex flex-col m-20 items-center justify-center  bg-[#2B2D3A] text-[#E6E6E6] font-sans">
      <div className="bg-[#313445] p-8 rounded-xl m-4 gap-5 max-w-md shadow-2xl border border-[#3D4052]">
        <div className="mb-8">
          <h1 className="text-xl font-semibold mb-6 text-center">
            Ověření vůči databázi neplatných dokladů
          </h1>
          <form className="flex flex-col gap-6">
            <div className="flex flex-col gap-2">
              <label htmlFor="doc" className="text-sm text-[#B6BAC5]">
                Číslo dokumentu
              </label>
              <input
                disabled={isSubmitting}
                id="doc"
                className="bg-[#2A2C39] border border-[#3D4052] text-[#E6E6E6] px-4 py-3 rounded-lg doc-number-input
                            focus:border-[#21BFC2] focus:ring-3 focus:ring-[#21BFC2]/30 outline-none transition"
                placeholder="např. AB123456"
                value={docNumber}
                onChange={(e) => setDocNumber(e.target.value)}
              />
            </div>
            {!isSubmitting && (
              <button
                disabled={!docNumber.trim()}
                className="bg-[#A77CFF] text-[#0E1015] font-semibold py-3
                        rounded-lg shadow-md disabled:opacity-60 disabled:cursor-not-allowed transition"
                onClick={handleSubmit}
              >
                Ověřit
              </button>
            )}
            {isSubmitting && (
              <>
                <ProgressBar
                  text={"Ověřuji..."}
                  checked={currentResults.length + 1}
                  total={total}
                />
                <button
                  onClick={handleCancel}
                  className="bg-[#A77CFF] text-[#0E1015] font-semibold py-3 rounded-lg shadow-md mt-4 transition hover:red-400 hover:pink-600"
                >
                  Zrušit
                </button>
              </>
            )}
            {currentResults.some(
              (cr) => cr.ResultType === ResultType.Invalid,
            ) && (
              <div className="flex items-center gap-3 bg-red-900/30 border-l-4 border-red-400 p-3 rounded shadow transition-all duration-300">
                <p>
                  Nalezen evidovaný dokument typu:
                </p>
                <p>
                    {getInvalidDocTypes(currentResults)}
                </p>
              </div>
            )}
          </form>
        </div>
        <ResultList fullResults={results} />
      </div>
    </div>
    </>
  );
}
