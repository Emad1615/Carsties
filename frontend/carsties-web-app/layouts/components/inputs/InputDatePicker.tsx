"use client";

import { Label, HelperText } from "flowbite-react";
import {
  FieldValues,
  useController,
  UseControllerProps,
} from "react-hook-form";
import "react-datepicker/dist/react-datepicker.css";
import { DatePicker } from "react-datepicker";
import type { DatePickerProps } from "react-datepicker";

type Props<T extends FieldValues> = {
  showLabel?: boolean;
  label?: string;
  id: string;
} & UseControllerProps<T> &
  DatePickerProps;
export default function InputDatePicker<T extends FieldValues>({
  showLabel,
  label,
  id,
  ...props
}: Props<T>) {
  const {
    field,
    fieldState: { isDirty, error },
  } = useController({ ...props });
  return (
    <div>
      {showLabel && (
        <div className="mb-2 block">
          <Label htmlFor={field.name}>{label}</Label>
        </div>
      )}
      <DatePicker
        startDate={new Date()}
        showTimeSelect
        showIcon
        selected={field.value ? new Date(field.value) : null}
        onChange={(date: Date | null) => field.onChange(date)}
        onBlur={field.onBlur}
        id={id}
        dateFormat="yyyy/MM/dd hh:mm aa"
        className={
          error
            ? "border border-red-400 rounded-lg"
            : !isDirty
              ? "border border-gray-400 rounded-lg"
              : "border border-green-400 rounded-lg"
        }
      />
      <HelperText className="text-red-500 text-sm">{error?.message}</HelperText>
    </div>
  );
}
