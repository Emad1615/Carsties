type PageResult<T> = {
  result: T[];
  pageCount: number;
  totalCount: number;
};
type Auction = {
  id: string;
  auctionId: number;
  reservePrice?: number;
  seller: string;
  winner?: string;
  soldAmount?: number;
  status: string;
  currentHighBid?: number;
  auctionEnd: string;
  createdAt: string;
  modifiedAt?: string;
  color: string;
  make: string;
  model: string;
  year: number;
  mileage: number;
  imageUrl: string;
};

type FilterOption = {
  label: string;
  value: string;
  icon?: React.ReactNode;
};
