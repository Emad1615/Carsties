export default function SearchBar() {
  return (
    <div className="relative   w-1/3 border border-gray-500 shadow-sm rounded-xl py-3 px-4">
      <input
        type="text"
        placeholder="Search for cars..."
        className="w-full border-none focus:ring-0 text-sm"
      />
      <button className="absolute right-3 top-3 text-gray-500 hover:text-gray-700">
        Search
      </button>
    </div>
  );
}
