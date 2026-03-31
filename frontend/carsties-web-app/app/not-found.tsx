import Heading from "@/layouts/components/Heading";
import { Button } from "flowbite-react";
import Link from "next/link";

export default function Index() {
  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <Heading
        title="404 Not found"
        subtitle="Page not found"
        textColor="orange"
        isMotion={false}
      />
      <Link
        href="/"
        className="border border-orange-500 text-orange-500 hover:bg-orange-500 hover:text-white transition-all duration-500  uppercase cursor-pointer  px-3 py-1 rounded font-semibold text-sm"
      >
        Back to home
      </Link>
    </div>
  );
}
