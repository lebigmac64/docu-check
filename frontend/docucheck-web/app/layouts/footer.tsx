export default function Footer() {
    return <footer style={{
        padding: "1rem 2rem",
        background: "#1a202c",
        color: "#fff",
        textAlign: "center",
        boxShadow: "0 -2px 4px rgba(0,0,0,0.05)"
    }}>
        © {new Date().getFullYear()} DocuCheck. All rights reserved.
    </footer>
}