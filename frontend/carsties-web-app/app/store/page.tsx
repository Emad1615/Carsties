"use client";
import { useState } from "react";
import { addAuction } from "../actions/auctions";
import { Spinner } from "flowbite-react";
import { error } from "console";

export default function Page() {
  const [loading, setLoading] = useState<boolean>(false);
  const [status, setStatus] = useState<number>();
  const [message, setMessage] = useState<string>("");
  const [error, setError] = useState();
  const handleClick = () => {
    setLoading(true);
    addAuction()
      .then((res) => {
        setStatus(res.status);
        setMessage(res.message);
      })
      .catch((res) => {
        setStatus(res.status);
        setMessage(res.message);
      })
      .finally(() => {
        setLoading(false);
      });
  };
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
      <p className="text-black"> Error : {error}</p>
    </div>
  );
}
