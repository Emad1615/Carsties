"use client";
import { useAuction } from "@/hooks/useAuction";
import AuctionForm from "./form/AuctionForm";
import Heading from "@/layouts/components/Heading";
import { Spinner } from "flowbite-react";

type Props = {
  id: number;
};
export default function UpdateAuction({ id }: Props) {
  const { auction, isLoading } = useAuction(id);
  return (
    <div className="text-black mx-auto shadow p-5 bg-white w-[75%] mt-24 border border-zinc-50">
      <Heading
        title="Edit car info"
        subtitle="Please update your car information by completing this form."
        isMotion={false}
        textColor="orange"
      />
      {isLoading ? (
        <div className="flex justify-center items-center mt-10">
          <Spinner size="lg" color="info" />
        </div>
      ) : (
        <AuctionForm auction={auction} />
      )}
    </div>
  );
}
