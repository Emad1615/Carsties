import { getAuction } from "@/app/actions/auctions";
import { useQuery } from "@tanstack/react-query";

export const useAuction = (id: number) => {
  const {
    data: auction,
    isLoading,
    error,
  } = useQuery<Auction>({
    queryKey: ["auction", id],
    queryFn: async () => await getAuction(id),
  });
  return { auction, isLoading, error };
};
