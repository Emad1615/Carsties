import Heading from "@/layouts/components/Heading";
import AuctionForm from "@/sections/auction/form/AuctionForm";

export default function page() {
  return (
    <div className="text-black mx-auto shadow p-5 bg-white w-[75%] mt-24 border border-zinc-50">
      <Heading
        title="Sell your car now"
        subtitle="Please provide your car information by completing this form."
        isMotion={false}
        textColor="orange"
      />
      <AuctionForm />
    </div>
  );
}
