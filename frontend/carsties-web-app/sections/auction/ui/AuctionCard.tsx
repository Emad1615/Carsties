import Image from "next/image";
import React from "react";
type Props = {
  isLoading: boolean;
  auction: Auction;
};
export default function AuctionCard({ auction, isLoading }: Props) {
  return (
    <div className="relative aspect-square object-cover  text-black">
      <Image
        src={auction.imageUrl}
        fill
        alt={`${auction.make}-${auction.model}-photo`}
        quality={100}
      />
    </div>
  );
}
