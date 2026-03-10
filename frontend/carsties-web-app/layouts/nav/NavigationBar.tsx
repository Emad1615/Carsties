import { Navbar, NavbarBrand } from "flowbite-react";
import Logo from "./Logo";
import UserActions from "./UserActions";
import { NavbarCollapse, NavbarLink } from "flowbite-react";
import { usePathname } from "next/navigation";
import { BiHome, BiNews, BiSearch, BiStore } from "react-icons/bi";

export function NavigationBar() {
  const currentPath = usePathname();

  return (
    <div className="shadow-md fixed top-0 left-0 right-0 z-50 ">
      <Navbar fluid className="container mx-auto">
        <NavbarBrand href="https://flowbite-react.com">
          <Logo />
        </NavbarBrand>
        <UserActions />
        <NavbarCollapse>
          <NavbarLink
            href="/"
            className={`flex justify-center items-center gap-1 ${currentPath === "/" ? "text-orange-500" : "text-gray-700"} `}
          >
            <BiHome /> Home
          </NavbarLink>
          <NavbarLink
            className={`flex justify-center items-center gap-1 ${currentPath === "/store" ? "text-orange-500" : "text-gray-700"}`}
            href="/store"
          >
            <BiStore />
            Store
          </NavbarLink>
          <NavbarLink
            className={`flex justify-center items-center gap-1 ${currentPath === "/feeds" ? "text-orange-500" : "text-gray-700"}`}
            href="#"
          >
            <BiNews />
            New Feeds
          </NavbarLink>
          <NavbarLink
            className={`flex justify-center items-center gap-1 ${currentPath === "/search" ? "text-orange-500" : "text-gray-700"}`}
            href="#"
          >
            <BiSearch />
            Search
          </NavbarLink>
        </NavbarCollapse>
      </Navbar>
    </div>
  );
}
