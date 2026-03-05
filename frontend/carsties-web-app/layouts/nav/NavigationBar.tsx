import {
  Avatar,
  Dropdown,
  DropdownDivider,
  DropdownHeader,
  DropdownItem,
  Navbar,
  NavbarBrand,
  NavbarCollapse,
  NavbarLink,
  NavbarToggle,
} from "flowbite-react";
import Logo from "./Logo";
import { BiHome, BiNews, BiSearch, BiStore } from "react-icons/bi";
import { usePathname } from "next/navigation";

export function NavigationBar() {
  const currentPath = usePathname();
  console.log(currentPath);
  return (
    <div className="shadow-md fixed top-0 left-0 right-0 z-50 ">
      <Navbar fluid className="container mx-auto">
        <NavbarBrand href="https://flowbite-react.com">
          <Logo />
        </NavbarBrand>
        <div className="flex md:order-2">
          <Dropdown
            arrowIcon={false}
            inline
            label={
              <Avatar
                alt="User settings"
                img="https://flowbite.com/docs/images/people/profile-picture-5.jpg"
                rounded
              />
            }
          >
            <DropdownHeader>
              <span className="block text-sm">Bonnie Green</span>
              <span className="block truncate text-sm font-medium">
                name@flowbite.com
              </span>
            </DropdownHeader>
            <DropdownItem>Dashboard</DropdownItem>
            <DropdownItem>Settings</DropdownItem>
            <DropdownItem>Earnings</DropdownItem>
            <DropdownDivider />
            <DropdownItem>Sign out</DropdownItem>
          </Dropdown>
          <NavbarToggle />
        </div>
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
