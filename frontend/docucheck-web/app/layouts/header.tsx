import { useState } from "react";
import {Link} from "react-router";

export default function Header() {
    const [active, setActive] = useState<"home" | "history">("home");

    return (
        <header className="flex items-center justify-between bg-[#313445] border-b border-[#3D4052] px-6 py-3 text-[#E6E6E6] font-sans">
            <div className="flex items-center gap-3">
                <div className="w-7 h-7 rounded-md bg-gradient-to-tr from-[#21BFC2] to-[#A77CFF] shadow-md" />
                <span className="text-base font-semibold tracking-wide">
          DocuCheck
        </span>
            </div>

            <nav className="flex gap-2">
                <Link
                    to="/"
                    onClick={() => setActive("home")}
                    aria-current={active === "home" ? "page" : undefined}
                    className={`px-4 py-2 rounded-lg text-sm font-medium transition ${
                        active === "home"
                            ? "bg-[#21BFC2]/20 border border-[#21BFC2] text-[#E6E6E6]"
                            : "text-[#B6BAC5] hover:text-[#E6E6E6] hover:bg-white/5"
                    }`}
                >
                    Home
                </Link>
                <Link
                    to="/history"
                    onClick={() => setActive("history")}
                    aria-current={active === "history" ? "page" : undefined}
                    className={`px-4 py-2 rounded-lg text-sm font-medium transition ${
                        active === "history"
                            ? "bg-[#21BFC2]/20 border border-[#21BFC2] text-[#E6E6E6]"
                            : "text-[#B6BAC5] hover:text-[#E6E6E6] hover:bg-white/5"
                    }`}
                >
                    History
                </Link>
            </nav>
        </header>
    );
}
