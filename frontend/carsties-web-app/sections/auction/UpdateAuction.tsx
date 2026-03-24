"use client";
import { useAuction } from "@/hooks/useAuction";

type Props = {
  id: number;
};
export default function UpdateAuction({ id }: Props) {
  const { auction, isLoading } = useAuction(id);
  console.log(auction);
  return <div>UpdateAuction</div>;
}
