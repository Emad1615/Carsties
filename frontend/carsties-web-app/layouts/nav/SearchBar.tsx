"use client";
import { ChangeEvent, KeyboardEvent, useState } from "react";
import { BiSearch } from "react-icons/bi";

export default function SearchBar() {
  const [query, setQuery] = useState<string>("");
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setQuery(e.target.value);
  };
  const handleSearch = () => {
    console.log("Searching for:", query);
  };

  return (
    <div className="relative w-10/12 mx-auto border border-gray-200 py-3 px-4 rounded shadow-sm mb-8">
      <input
        value={query}
        onKeyDown={(e: KeyboardEvent) => {
          if (e.key === "Enter") {
            handleSearch();
            console.log("Searching for:", query);
          }
        }}
        onChange={handleChange}
        type="text"
        placeholder="Search for cars by make, model or color"
        className="w-full   outline-none text-zinc-500 font-semibold text-sm "
      />
      <button
        onClick={() => handleSearch()}
        className="cursor-pointer transition duration-200 hover:scale-125  px-4 py-2 rounded absolute right-0 top-1/2 transform -translate-y-1/2"
      >
        <BiSearch size={20} color="red" />
      </button>
    </div>
  );
}
