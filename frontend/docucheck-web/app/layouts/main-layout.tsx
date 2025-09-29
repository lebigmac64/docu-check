import { Outlet } from "react-router";
import Header from "~/layouts/header";
import Footer from "~/layouts/footer";

export default function MainLayout() {
  return (
      <>
    <div className="min-h-screen">
      <Header />
      <main>
        <Outlet />
      </main>
    </div>
    <Footer />
      </>
  );
}
