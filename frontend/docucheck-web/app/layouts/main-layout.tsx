import { Outlet } from "react-router";
import Header from "~/layouts/header";
import Footer from "~/layouts/footer";

export default function MainLayout() {
    return (<>
            <Header />
                <Outlet />
            <Footer />
        </>
    );
}
