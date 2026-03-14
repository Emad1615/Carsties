import Empty from "@/layouts/components/Empty";
type Props = {
  searchParams: {
    callbackUrl: string;
  };
};
export default function page({ searchParams }: Props) {
  return (
    <div>
      <Empty
        title="You need to logged in to do that"
        subtitle="Please click blow to login "
        showLogin={true}
        callbackUrl={searchParams.callbackUrl}
      />
    </div>
  );
}
