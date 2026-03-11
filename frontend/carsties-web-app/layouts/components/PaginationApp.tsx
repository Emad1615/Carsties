import { useParamStore } from "@/hooks/useParamStore";
import { Pagination, Select } from "flowbite-react";
type Props = {
  pageCount: number;
  pageNumber: number;
  onPageChange: (page: number) => void;
};
export default function PaginationApp({
  pageNumber,
  onPageChange,
  pageCount,
}: Props) {
  const pageSize = useParamStore((state) => state.pageSize);
  const setPageSize = useParamStore((state) => state.setParam);
  return (
    <div className="flex justify-center items-end-safe gap-4">
      <div className="flex overflow-x-auto sm:justify-center mt-6">
        <Pagination
          layout="pagination"
          currentPage={pageNumber}
          totalPages={pageCount || 1}
          onPageChange={onPageChange}
          showIcons
        />
      </div>

      <Select
        value={pageSize}
        id="pageSize"
        onChange={(e) => setPageSize({ pageSize: Number(e.target.value) })}
        className="w-16 "
      >
        <option value={16}>16</option>
        <option value={24}>24</option>
        <option value={32}>32</option>
        <option value={42}>42</option>
      </Select>
    </div>
  );
}
