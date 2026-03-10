import CountDownComponent from "@/layouts/components/CountDownComponent";
import { Avatar } from "flowbite-react";
import Image from "next/image";
import React from "react";
import { HiOutlineHeart } from "react-icons/hi";
type Props = {
  auction: Auction;
};
export default function AuctionCard({ auction }: Props) {
  return (
    <div className="text-black bg-zinc-50 rounded-md shadow-md  transition-transform duration-500 hover:scale-105 ">
      <div className="relative aspect-video object-cover  ">
        <Image
          src={auction.imageUrl}
          fill
          alt={`${auction.make}-${auction.model}-photo`}
          quality={75}
          className="rounded-md"
          sizes="(max-width: 768px) 100vw, 
                 (max-width: 1200px) 50vw, 
                  33vw"
          loading="eager"
        />
        <CountDownComponent auctionDate={auction.auctionEnd} />
      </div>
      <div className="py-3 px-2 flex flex-col gap-2">
        <div className="flex justify-between items-center">
          <div className="flex  items-center gap-1">
            <Avatar
              size="sm"
              //   img="/images/people/profile-picture-5.jpg"
              alt={auction.seller}
              rounded
            />
            <span className="text-zinc-500 text-sm font-semibold">
              {auction.seller}
            </span>
          </div>
          <HiOutlineHeart
            size={"25"}
            className="text-orange-500 transition-colors duration-200 hover:text-orange-600"
            onClick={() => alert("saved in liked list")}
          />
        </div>
        <div className="flex justify-between items-center ">
          <div className="flex flex-col gap-1">
            <span className="text-md text-gray-700 font-semibold">
              {auction.make} - {auction.year}
            </span>
            <span className="text-xs text-gray-500 font-semibold">
              {auction.model}
            </span>
          </div>
          <span className="text-xs font-medium text-zinc-600">
            {auction.mileage} Miles
          </span>
        </div>
      </div>
    </div>
  );
}
