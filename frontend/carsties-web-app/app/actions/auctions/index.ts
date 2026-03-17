"use server";

import { createAgent } from "@/lib/agent";
import { FieldValues } from "react-hook-form";

const agent = await createAgent();

export const getAuctions = async (url: string) => {
  return agent
    .get<PageResult<Auction>>(`/search?${url}`)
    .then((response) => response.data);
};

export const AddAuction = async (data: FieldValues) => {
  return agent.post("/auction", data).then((response) => response.data);
};
export const UpdateAuction = async (data: FieldValues, id: number) => {
  return agent.put(`/auction/${id}`, data).then((response) => response.data);
};

export const DeleteAuction = async (id: number) => {
  return agent.delete(`/auction/${id}`).then((response) => response.data);
};
