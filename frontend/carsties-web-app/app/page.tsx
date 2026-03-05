import Heading from "@/layouts/components/Heading";
import SearchBar from "@/layouts/nav/SearchBar";
import AuctionView from "@/sections/auction/AuctionList";

export const revalidate = 0;
export default function Home() {
  return (
    <div className="container mx-auto px-4 py-8 ">
      <Heading
        title="Welcome to Carsties!"
        subtitle="Explore exclusive car auctions, place your bids, and win your next ride at the best possible price. Join Carsties and start bidding now!"
        textColor="orange"
        isMotion={true}
      />
      <SearchBar />
      <AuctionView />
    </div>
  );
}
