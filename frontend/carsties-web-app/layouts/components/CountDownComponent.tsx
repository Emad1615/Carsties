import Countdown, { zeroPad } from "react-countdown";

// Renderer callback with condition
const renderer = ({
  days,
  hours,
  minutes,
  seconds,
  completed,
}: {
  days: number;
  hours: number;
  minutes: number;
  seconds: number;
  completed: boolean;
}) => {
  return (
    <div
      className={`absolute bottom-2 left-2 rounded-md shadow-sm px-2 pb-1 tracking-widest ${completed ? "bg-red-500" : days === 0 && hours < 6 ? "bg-orange-500" : "bg-green-600"}`}
    >
      {completed ? (
        <span className="text-xs font-semibold text-white">
          Auction finished
        </span>
      ) : (
        <span
          className="text-xs text-white font-semibold"
          suppressHydrationWarning={true}
        >
          {zeroPad(days)}:{zeroPad(hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}
        </span>
      )}
    </div>
  );
};
type Props = {
  auctionDate: string;
};
export default function CountDownComponent({ auctionDate }: Props) {
  return <Countdown date={auctionDate} renderer={renderer} />;
}
