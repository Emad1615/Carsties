import Filter from "@/layouts/components/Filter";
import { CgLock } from "react-icons/cg";
import { FaClock, FaSortAlphaDown, FaStopCircle } from "react-icons/fa";

const orderFilters: FilterOption[] = [
  { label: "Alphabetical", value: "make", icon: <FaSortAlphaDown /> },
  { label: "Recently added", value: "new", icon: <FaStopCircle /> },
  { label: "End date", value: "end_date", icon: <FaClock /> },
];
export default function AuctionFilters() {
  return (
    <div className="flex justify-between items-center">
      <Filter
        filters={orderFilters}
        currentFilter=""
        onFilterChange={() => {}}
      />
    </div>
  );
}
