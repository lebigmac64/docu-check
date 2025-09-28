import type {Route} from "../../.react-router/types/app/routes/+types/history";

export function meta({}: Route.MetaArgs) {
    return [
        { title: "Historie kontrol" },
        { name: "description", content: "Historie kontrol dokumentů" },
    ];
}

export default function History() {
    return <h1>Historie kontrol</h1>
}
