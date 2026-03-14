"use client";
import Heading from "./Heading";
import { useParamStore } from "@/hooks/useParamStore";
import { signIn } from "next-auth/react";
type Props = {
  title?: string;
  subtitle?: string;
  isReset?: boolean;
  showLogin?: boolean;
  callbackUrl?: string;
};
export default function Empty({
  title = "No cars match your search",
  subtitle = "Try different keywords or reset your filters to see more results.",
  isReset,
  showLogin,
  callbackUrl,
}: Props) {
  const reset = useParamStore((state) => state.reset);
  return (
    <div className="flex justify-center items-center flex-col gap-2 py-12 shadow-md bg-zinc-50 border border-zinc-100     rounded-md my-5">
      <Heading
        isMotion={false}
        title={title}
        textColor="zinc"
        subtitle={subtitle}
      />
      {isReset && (
        <button
          onClick={() => reset()}
          className="bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-md font-semibold text-sm duration-300 focus:outline-none focus:ring-none "
        >
          Back to Home
        </button>
      )}
      {showLogin && (
        <button
          onClick={() =>
            signIn(
              "id-server",
              { redirectTo: callbackUrl },
              { prompt: "login" },
            )
          }
          className="bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-md font-semibold text-sm duration-300 focus:outline-none focus:ring-none "
        >
          Login
        </button>
      )}
    </div>
  );
}
