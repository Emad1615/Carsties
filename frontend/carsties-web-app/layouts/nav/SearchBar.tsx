"use client";
import { useParamStore } from "@/hooks/useParamStore";
import { ChangeEvent, KeyboardEvent, useEffect, useState } from "react";
import { BiSearch } from "react-icons/bi";
import { FaRegTrashAlt } from "react-icons/fa";
import { useShallow } from "zustand/shallow";

export default function SearchBar() {
  const setParam = useParamStore(
    useShallow((state) => ({ setParam: state.setParam, reset: state.reset })),
  );
  const searchTerm = useParamStore((state) => state.searchTerm);
  const [query, setQuery] = useState<string>("");
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setQuery(e.target.value);
  };
  const handleSearch = () => {
    setParam.setParam({ searchTerm: query });
    console.log("Searching for:", query);
  };
  useEffect(() => {
    const handlePress = (e: globalThis.KeyboardEvent) => {
      if (e.key == "Escape") {
        setParam.reset();
        setQuery("");
      }
    };
    document.addEventListener("keydown", handlePress);
    return () => document.removeEventListener("keydown", handlePress);
  }, []);
  return (
    <div className="relative w-11/12 mx-auto border border-gray-200 py-3 px-4 rounded shadow-sm mb-8">
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

      {searchTerm ? (
        <button
          onClick={() => {
            setParam.reset();
            setQuery("");
          }}
          className="cursor-pointer transition duration-200 hover:scale-125  px-4 py-2 rounded absolute right-0 top-1/2 transform -translate-y-1/2"
        >
          <FaRegTrashAlt size={25} className="text-orange-500 font-bold" />
        </button>
      ) : (
        <button
          onClick={() => {
            handleSearch();
          }}
          className="cursor-pointer transition duration-200 hover:scale-125  px-4 py-2 rounded absolute right-0 top-1/2 transform -translate-y-1/2"
        >
          <BiSearch size={25} className="text-orange-500 font-bold" />
        </button>
      )}
    </div>
  );
}
