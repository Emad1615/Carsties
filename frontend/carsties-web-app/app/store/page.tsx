"use client";
import toast from "react-hot-toast";

export default function page() {
  return (
    <div>
      <button
        className="text-black bg-orange-400 p-2"
        onClick={() => {
          toast.success("Successfully");
        }}
      >
        Click Me Success
      </button>

      <button
        className="text-black bg-orange-400 p-2"
        onClick={() => {
          toast.error("Failure");
        }}
      >
        Click Me Fail
      </button>
    </div>
  );
}
