import { UpdateAuction } from "@/app/actions/auctions";
import { useMutation } from "@tanstack/react-query";
import { FieldValues } from "react-hook-form";

export const useUpdateAuction = () => {
  const { mutate: editAuction, isPending } = useMutation({
    mutationFn: async (data: FieldValues) => await UpdateAuction(data),
  });
  return { editAuction, isPending };
};
