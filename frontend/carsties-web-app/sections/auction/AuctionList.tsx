"use client";
import AuctionFilters from "./ui/AuctionFilters";
import { useAuctions } from "@/hooks/useAuctions";
import { useParamStore } from "@/hooks/useParamStore";
import { useShallow } from "zustand/shallow";
import qs from "query-string";
import Spinner from "@/layouts/components/Spinner";
import AuctionCard from "./ui/AuctionCard";
import PaginationApp from "@/layouts/components/PaginationApp";
import Empty from "@/layouts/components/Empty";
export default function AuctionView() {
  const params = useParamStore(
    useShallow((state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy,
    })),
  );
  const setParam = useParamStore((state) => state.setParam);
  const queryStringUrl = qs.stringify(
    { url: "", ...params },
    { skipEmptyString: true },
  );
  const { auctions, isLoading } = useAuctions(queryStringUrl);
  console.log(auctions);
  return (
    <div>
      <AuctionFilters />
      {isLoading && <Spinner />}
      {auctions && (
        <>
          {auctions.result.length > 0 ? (
            <div className="grid sm:grid-cols-3 md:grid-cols-4 gap-5 my-12 ">
              {auctions.result.map((data: Auction) => (
                <AuctionCard key={data.id} auction={data} />
              ))}
            </div>
          ) : (
            <Empty isReset={true} />
          )}

          <PaginationApp
            pageCount={auctions.pageCount}
            pageNumber={params.pageNumber}
            onPageChange={(value) => setParam({ pageNumber: value })}
          />
        </>
      )}
    </div>
  );
}
