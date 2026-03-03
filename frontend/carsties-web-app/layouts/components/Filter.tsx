"use client";
type Props = {
  filters: FilterOption[];
  currentFilter: string;
  onFilterChange: (filter: string) => void;
};
export default function Filter({
  filters,
  currentFilter,
  onFilterChange,
}: Props) {
  return (
    <div className="flex items-center">
      <span className="mr-4 text-sm font-semibold">Order by:</span>
      {filters.map((filter) => (
        <button
          key={filter.value}
          className={`px-4 py-2  text-xs font-medium flex justify-center items-center gap-2 ${
            currentFilter === filter.value
              ? "bg-blue-500 text-white"
              : "bg-gray-200 text-gray-700 hover:bg-gray-300"
          }`}
          onClick={() => onFilterChange(filter.value)}
        >
          {filter.icon}
          {filter.label}
        </button>
      ))}
    </div>
  );
}
