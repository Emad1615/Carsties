import Image from "next/image";
import React from "react";

export default function Logo() {
  return (
    <div className="flex gap-1 items-center justify-center bg-orange-100 rounded-r-full rounded-tl-full px-2">
      <Image src="/icon.png" alt="Logo" width={50} height={50} quality={75} />
      <span className="text-orange-500 text-sm font-semibold italic tracking-widest">
        Carsties
      </span>
    </div>
  );
}
