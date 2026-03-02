import { Pagination } from "flowbite-react";
type Props = {
  pageNumber: number;
  onPageChange: (page: number) => void;
};
export default function PaginationApp({ pageNumber, onPageChange }: Props) {
  return (
    <div className="flex overflow-x-auto sm:justify-center">
      <Pagination
        layout="pagination"
        currentPage={pageNumber}
        totalPages={1000}
        onPageChange={onPageChange}
        showIcons
      />
    </div>
  );
}
