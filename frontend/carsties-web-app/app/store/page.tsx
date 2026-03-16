"use client";
import { useState } from "react";
import { addAuction } from "../actions/auctions";
import { Spinner } from "flowbite-react";

export default function Page() {
  const [loading, setLoading] = useState<boolean>(false);
  const [status, setStatus] = useState<number>();
  const [message, setMessage] = useState<string>("");
  const [error, setError] = useState<string[]>([]);
  const handleClick = () => {
    setLoading(true);
    addAuction()
      .then((res) => {
        setStatus(res.status);
        setMessage(res.message);
        setError(res.errors as string[]);
      })
      .catch((res) => {
        setStatus(res.status);
        setMessage(res.message);
        setError(res.errors as string[]);
      })
      .finally(() => {
        setLoading(false);
      });
  };
  console.log(error);
  return (
    <div>
      <button
        className=" bg-orange-400 p-2 min-w-16 rounded text-white text-sm font-semibold cursor-pointer"
        onClick={handleClick}
      >
        {loading ? (
          <>
            <Spinner size="sm" color="warning" />
            <span className="text-xs text-white "> loading...</span>
          </>
        ) : (
          <>Add Auction</>
        )}
      </button>
      <p className="text-black">Status : {status}</p>
      <p className="text-black"> Message : {message}</p>
      {error.map((e, i) => (
        <p key={i} className="text-red-500">
          {e}
        </p>
      ))}
    </div>
  );
}
