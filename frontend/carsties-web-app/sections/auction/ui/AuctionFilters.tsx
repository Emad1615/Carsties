import { useParamStore } from "@/hooks/useParamStore";
import Filter from "@/layouts/components/Filter";
import { FaClock, FaSortAlphaDown, FaStopCircle } from "react-icons/fa";
import { GiSmallFire } from "react-icons/gi";
import { ImHourGlass } from "react-icons/im";
import { IoCheckmarkDoneCircleOutline } from "react-icons/io5";
import { useShallow } from "zustand/shallow";

const orderFilters: FilterOption[] = [
  { label: "Alphabetical", value: "make", icon: <FaSortAlphaDown /> },
  { label: "Recently added", value: "new", icon: <FaStopCircle /> },
  { label: "End date", value: "end_date", icon: <FaClock /> },
];
const additionalFilters: FilterOption[] = [
  { label: "Done", value: "finished", icon: <IoCheckmarkDoneCircleOutline /> },
  { label: "Ending soon < 6", value: "endingSoon", icon: <ImHourGlass /> },
  { label: "Live", value: "live", icon: <GiSmallFire /> },
];

export default function AuctionFilters() {
  const params = useParamStore(
    useShallow((state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
      orderBy: state.orderBy,
      filterBy: state.filterBy,
    })),
  );
  const setParam = useParamStore((state) => state.setParam);
  return (
    <div className="flex flex-col  gap-3 sm:flex-row sm:justify-between items-center ">
      <Filter
        filters={orderFilters}
        currentFilter={params.orderBy}
        onFilterChange={(value) => setParam({ orderBy: value })}
      />
      <Filter
        filters={additionalFilters}
        currentFilter={params.filterBy}
        onFilterChange={(value) => setParam({ filterBy: value })}
      />
    </div>
  );
}
