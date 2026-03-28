import AuctionDetails from "@/sections/auction/AuctionDetails";
import { Dropdown, DropdownItem } from "flowbite-react";
import { GiCogsplosion } from "react-icons/gi";
import { MdEdit } from "react-icons/md";
import { AiFillDelete } from "react-icons/ai";
import Link from "next/link";

export default async function page({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  return (
    <div className="text-black flex flex-col gap-5 ">
      <div className="flex justify-end  ">
        <Dropdown
          label={
            <>
              <GiCogsplosion size="25" />
            </>
          }
          outline
          className="border-b border-b-orange-500 rounded-none outline-none focus:border-none focus:outline-none focus:ring-0  text-orange-500"
          color={"alternative"}
        >
          <DropdownItem className="flex gap-3 justify-start items-center">
            <MdEdit />
            <Link href={`/auction/update/${id}`}> Edit</Link>
          </DropdownItem>
          <DropdownItem className="flex gap-3 justify-start items-center">
            <AiFillDelete />
            Delete
          </DropdownItem>
        </Dropdown>
      </div>

      <AuctionDetails />
    </div>
  );
}
