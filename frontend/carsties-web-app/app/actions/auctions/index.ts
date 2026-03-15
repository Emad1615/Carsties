"use server";

import { auth } from "@/auth";

const apiUrl = process.env.NEXT_PUBLIC_API_URL;

export const getAuctions = async (url: string) => {
  return await fetch(`${apiUrl}/search?${url}`).then((res) => {
    if (!res.ok) {
      throw new Error("Failed to fetch auctions");
    }
    return res.json() as Promise<PageResult<Auction>>;
  });
};

export const addAuction = async () => {
  const session = await auth();
  console.log(session?.accessToken);
  const data = {
    reservePrice: 1650000,
    auctionEnd: "2026-03-01T10:01:31.507716Z",
    imageUrl:
      "https://contactcars.fra1.cdn.digitaloceanspaces.com/contactcars-production/Images/Small/Engines/2df21740a3fd_c542b7ae-8a9f-4338-8046-40ede0a1b0eb.jpeg",
    color: "Satin Schist Grey",
    make: "Renault ",
    model: "Austral",
    year: 2026,
    mileage: 5,
  };
  const result = await fetch(`${apiUrl}/auction`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${session?.accessToken}`,
    },
    body: JSON.stringify(data),
  });
  const res = await result.json();
  console.log(res);
  if (!result.ok)
    return {
      status: result.status,
      message: result.statusText,
      error: res,
    };

  return {
    status: result.status,
    message: result.statusText,
    error: null,
  };
};
