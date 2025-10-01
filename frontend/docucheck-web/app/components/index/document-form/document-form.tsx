import ResultList from "~/components/index/result-list/result-list";
import DocumentInput from "~/components/index/document-input/document-input";
import {useState} from "react";
import type {FullResult} from "~/components/index/document-form/document-form.module";

export default function DocumentForm(): React.ReactElement {
    const [results, setResults] = useState([] as FullResult[]);

  return (
      <>
    <div className="flex flex-col m-20 items-center justify-center  bg-[#2B2D3A] text-[#E6E6E6] font-sans">
      <div className="bg-[#313445] p-8 rounded-xl m-4 gap-5 max-w-md shadow-2xl border border-[#3D4052]">
        <DocumentInput onResultsChanged={setResults}/>
        <ResultList fullResults={results} />
      </div>
    </div>
    </>
  );
}
