import React, { useState } from "react";

export default function Checker() : React.ReactElement {
    const [doc, setDoc] = useState("");

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
                            className="bg-[#2A2C39] border border-[#3D4052] text-[#E6E6E6] px-4 py-3 rounded-lg focus:border-[#21BFC2] focus:ring-2 focus:ring-[#21BFC2]/30 outline-none transition"
                            placeholder="e.g., 123456789 or AB123456"
                            value={doc}
                            onChange={(e) => setDoc(e.target.value)}
                        />
                    </div>
                    <button
                        disabled={!doc.trim()}
                        className="bg-gradient-to-r from-[#21BFC2] to-[#A77CFF] text-[#0E1015] font-semibold py-3 rounded-lg shadow-md disabled:opacity-60 disabled:cursor-not-allowed transition"
                    >
                        Check now
                    </button>
                </form>
            </div>
        </div>
    );
}
