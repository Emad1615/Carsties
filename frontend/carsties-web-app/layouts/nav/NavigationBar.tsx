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
import SearchBar from "./SearchBar";

export function NavigationBar() {
  return (
    <Navbar>
      <NavbarBrand href="/">
        {/* eslint-disable-next-line */}
        <img
          src="/icon.png"
          className="mr-1 h-6 sm:h-10"
          alt="Flowbite React Logo"
        />
        <span className="self-center whitespace-nowrap text-xs font-semibold text-orange-500 uppercase italic">
          Carsties
        </span>
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
        <SearchBar />
      </NavbarCollapse>
    </Navbar>
  );
}
