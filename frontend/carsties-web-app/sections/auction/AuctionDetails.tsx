"use client";

import { useAuction } from "@/hooks/useAuction";
import AuctionTable from "./ui/AuctionTable";
import Spinner from "@/layouts/components/Spinner";
import AuctionGallery from "./ui/AuctionGallery";

type Props = {
  id: number;
};
export default function AuctionDetails({ id }: Props) {
  const { auction, isLoading } = useAuction(id);
  if (isLoading) {
    return <Spinner />;
  }
  return (
    <div className="">
      <div className="border border-zinc-50 p-3 shadow rounded grid grid-cols-2 gap-4 ">
        <AuctionGallery />
      </div>

      <AuctionTable auction={auction} />
    </div>
  );
}
