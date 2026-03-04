const apiUrl = process.env.NEXT_PUBLIC_API_URL;

export const getAuctions = async (url: string) => {
  return await fetch(`${apiUrl}/search?${url}`).then((res) => {
    if (!res.ok) {
      throw new Error("Failed to fetch auctions");
    }
    return res.json() as Promise<PageResult<Auction>>;
  });
};
