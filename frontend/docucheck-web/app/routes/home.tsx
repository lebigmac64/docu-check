import DocumentForm from "~/components/document-form";
import type {Route} from "../../.react-router/types/app/routes/+types/home";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Kontrola platnosti dokumentu" },
    { name: "description", content: "Ověřte platnost dokumentu" },
  ];
}

export default function Home() {
  return <DocumentForm />;
}
