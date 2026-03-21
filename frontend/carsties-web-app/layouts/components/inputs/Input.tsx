"use client";
import { Label, TextInput, HelperText } from "flowbite-react";
import {
  FieldValues,
  useController,
  UseControllerProps,
} from "react-hook-form";

type Props<T extends FieldValues> = {
  showLabel?: boolean;
  label?: string;
  id: string;
  type?: string | "text";
} & UseControllerProps<T>;
export default function Input<T extends FieldValues>({
  showLabel,
  label,
  id,
  type,
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
      <TextInput
        {...field}
        value={field.value ?? ""}
        id={id}
        type={type}
        color={error ? "failure" : !isDirty ? "gray" : "success"}
      />
      <HelperText className="text-red-500 text-sm">{error?.message}</HelperText>
    </div>
  );
}
