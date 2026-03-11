"use client";
import { useCurrentUser } from "@/hooks/useCurrentUser";
import {
  Avatar,
  Button,
  Dropdown,
  DropdownDivider,
  DropdownHeader,
  DropdownItem,
  NavbarToggle,
  Spinner,
} from "flowbite-react";
import { signIn } from "next-auth/react";

export default function UserActions() {
  const { user, isLoading } = useCurrentUser();
  return (
    <div className="flex md:order-2">
      {user ? (
        <>
          {isLoading ? (
            <Spinner size="xs" />
          ) : (
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
              <DropdownItem>My auctions</DropdownItem>
              <DropdownItem>Auction won</DropdownItem>
              <DropdownItem>Sell my car</DropdownItem>
              <DropdownDivider />
              <DropdownItem>Sign out</DropdownItem>
            </Dropdown>
          )}
        </>
      ) : (
        <>
          <Button
            size="xs"
            onClick={() =>
              signIn("id-server", { redirectTo: "/" }, { prompt: "login" })
            }
            className="border border-orange-500 text-orange-500 hover:border-orange-500 hover:bg-orange-500 duration-300 transition-colors cursor-pointer"
            outline
          >
            Login
          </Button>
        </>
      )}

      <NavbarToggle />
    </div>
  );
}
