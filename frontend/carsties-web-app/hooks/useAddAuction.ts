import { AddAuction } from "@/app/actions/auctions";
import { useMutation } from "@tanstack/react-query";
import { FieldValues } from "react-hook-form";

export const useAddAuction = () => {
  const { mutate: saveAuction, isPending } = useMutation({
    mutationFn: async (data: FieldValues) => await AddAuction(data),
  });
  return { saveAuction, isPending };
};
