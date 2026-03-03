"use client";

import { Button, ButtonGroup } from "flowbite-react";

type Props = {
  label: string;
  filters: FilterOption[];
  currentFilter: string;
  onFilterChange: (filter: string) => void;
};
export default function Filter({
  label,
  filters,
  currentFilter,
  onFilterChange,
}: Props) {
  return (
    <div className="flex items-center">
      <span className="mr-4 text-xs font-semibold text-zinc-500 uppercase">
        {label}
      </span>
      <ButtonGroup outline={true}>
        {filters.map((filter) => (
          <Button
            size="xs"
            key={filter.value}
            onClick={() => onFilterChange(filter.value)}
            color={`${currentFilter == filter.value ? "red" : "gray"}`}
            className="text-xs flex gap-1 items-center justify-center"
          >
            {filter.icon}
            {filter.label}
          </Button>
        ))}
      </ButtonGroup>
    </div>
  );
}
