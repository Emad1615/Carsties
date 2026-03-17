import { Label, TextInput, HelperText } from "flowbite-react";
import { useController, UseControllerProps } from "react-hook-form";

type Props = {
  showLabel?: boolean;
  label?: string;
  id: string;
  type?: string | "text";
} & UseControllerProps;
export default function Input({ showLabel, label, id, type, ...props }: Props) {
  const {
    field,
    fieldState: { isDirty, invalid, error },
  } = useController({ ...props });
  return (
    <div>
      {showLabel && (
        <div className="mb-2 block">
          <Label htmlFor="email1">{label}</Label>
        </div>
      )}
      <TextInput
        {...field}
        value={field.value ?? ""}
        id={id}
        type={type}
        className=""
      />
      <HelperText>{error?.message}</HelperText>
    </div>
  );
}
