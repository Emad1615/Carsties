"use client";

import { Button, ButtonGroup } from "flowbite-react";

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
    <ButtonGroup outline={true}>
      {filters.map((filter) => (
        <Button
          size="xs"
          key={filter.value}
          onClick={() =>
            onFilterChange(filter.value == currentFilter ? "" : filter.value)
          }
          color={`${currentFilter == filter.value ? "red" : "gray"}`}
          className={`text-xs flex gap-1 items-center justify-center `}
        >
          {filter.icon}
          {filter.label}
        </Button>
      ))}
    </ButtonGroup>
  );
}
