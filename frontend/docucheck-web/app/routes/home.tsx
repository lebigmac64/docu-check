import Checker from "~/components/checker/checker";
import type { Route } from "./+types/home";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Kontrola platnosti dokumentu" },
    { name: "description", content: "Ověřte platnost dokumentu" },
  ];
}

export default function Home() {
  return <Checker className="flex justify-center flex-col items-center h-screen" />;
}
