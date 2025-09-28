import {Link} from "react-router";

export default function Header() {
    return <header style={{
        display: "flex",
        alignItems: "center",
        justifyContent: "space-between",
        padding: "1rem 2rem",
        background: "#1a202c",
        color: "#fff",
        boxShadow: "0 2px 4px rgba(0,0,0,0.05)"
        }}>
        <div style={{ fontWeight: "bold", fontSize: "1.5rem" }}>
            DocuCheck
        </div>
        <nav>
            <Link to="/" style={{ color: "#fff", marginRight: "1.5rem", textDecoration: "none" }}>Home</Link>
            <Link to="/history" style={{ color: "#fff", marginRight: "1.5rem", textDecoration: "none" }}>History</Link>
            <Link to="/about" style={{ color: "#fff", textDecoration: "none" }}>About</Link>
        </nav>
    </header>
}