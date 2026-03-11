import { getAuctions } from "@/app/actions/auctions";
import { useQuery } from "@tanstack/react-query";

export const useAuctions = (url: string) => {
  const { data: auctions, isLoading } = useQuery({
    queryKey: ["auctions", url],
    queryFn: async () => await getAuctions(url),
  });
  return { auctions, isLoading };
};
