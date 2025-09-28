import { Outlet } from "react-router";
import Header from "~/layouts/header";
import Footer from "~/layouts/footer";

export default function MainLayout() {
    return (
        <div>
            <Header />
            <main style={{ flex: 1 }}>
                <Outlet />
            </main>
            <Footer />
        </div>
    );
}
