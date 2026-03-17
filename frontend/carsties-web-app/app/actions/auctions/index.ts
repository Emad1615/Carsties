"use server";

import { createAgent } from "@/lib/agent";

const agent = await createAgent();

export const getAuctions = async (url: string) => {
  return agent
    .get<PageResult<Auction>>(`/search?${url}`)
    .then((response) => response.data);
};

export const AddAuction = async (data: any) => {
  return agent.post("/auction", data).then((response) => response.data);
};
export const UpdateAuction = async (data: any, id: number) => {
  return agent.put(`/auction/${id}`, data).then((response) => response.data);
};

export const DeleteAuction = async (id: number) => {
  return agent.delete(`/auction/${id}`).then((response) => response.data);
};
