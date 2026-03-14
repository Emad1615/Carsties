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
import { signIn, signOut } from "next-auth/react";
import { BiUser } from "react-icons/bi";

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
                <div className="flex justify-start items-center gap-1 ">
                  <Avatar
                    alt={user.name!}
                    img={user.image!}
                    rounded
                    className="border-2 border-orange-500 rounded-full"
                  />
                  <span className="text-zinc-600 font-semibold text-sm uppercase ">
                    {user.name}
                  </span>
                </div>
              }
            >
              <DropdownHeader className="border-zinc-100 border-b ">
                <span className="flex justify-start items-center gap-1 truncate  text-sm ">
                  <BiUser /> {user.username}
                </span>
                <span className=" text-sm  w-32 tracking-widest font-bold">
                  {user.email}
                </span>
              </DropdownHeader>
              <DropdownItem>My auctions</DropdownItem>
              <DropdownItem>Auction won</DropdownItem>
              <DropdownItem>Sell my car</DropdownItem>
              <DropdownDivider />
              <DropdownItem onClick={() => signOut({ redirectTo: "/" })}>
                Sign out
              </DropdownItem>
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
