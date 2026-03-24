import UpdateAuction from "@/sections/auction/UpdateAuction";

export default async function page({
  params,
}: {
  params: Promise<{ id: number }>;
}) {
  const { id } = await params;
  return <UpdateAuction id={id} />;
}
