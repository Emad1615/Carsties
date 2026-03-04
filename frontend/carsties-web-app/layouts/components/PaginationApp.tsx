import { Pagination } from "flowbite-react";
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
  return (
    <div className="flex overflow-x-auto sm:justify-center mt-6">
      <Pagination
        layout="pagination"
        currentPage={pageNumber}
        totalPages={pageCount}
        onPageChange={onPageChange}
        showIcons
      />
    </div>
  );
}
