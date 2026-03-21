"use client";
import { FieldValues, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  auctionSchema,
  AuctionSchemaInput,
  AuctionSchemaOutput,
} from "./schema/auctionSchema";
import Input from "@/layouts/components/inputs/Input";
import InputDatePicker from "@/layouts/components/inputs/InputDatePicker";

export default function AuctionForm() {
  const {
    control,
    handleSubmit,
    formState: { isSubmitting, isValid, errors },
    reset,
  } = useForm<AuctionSchemaInput, FieldValues, AuctionSchemaOutput>({
    mode: "onTouched",
    resolver: zodResolver(auctionSchema),
  });
  const onHandleSubmit = (data: FieldValues) => {
    console.log(data);
  };
  return (
    <form onSubmit={handleSubmit(onHandleSubmit)}>
      <Input
        control={control}
        id="makeId"
        name="make"
        showLabel={true}
        label="Make"
        type="text"
      />
      <Input
        control={control}
        id="modelId"
        name="model"
        showLabel={true}
        label="Model"
        type="text"
      />
      <Input
        control={control}
        id="colorId"
        name="color"
        showLabel={true}
        label="Color"
        type="text"
      />
      <Input
        control={control}
        id="yearId"
        name="year"
        showLabel={true}
        label="Year"
        type="number"
      />
      <Input
        control={control}
        id="mileageId"
        name="mileage"
        showLabel={true}
        label="Mileage"
        type="number"
      />
      <Input
        control={control}
        id="reservePriceId"
        name="reservePrice"
        showLabel={true}
        label="Reserve Price"
        type="number"
      />
      <InputDatePicker
        control={control}
        id="auctionEndId"
        name="auctionEnd"
        showLabel={true}
        label="Auction end date"
      />
    </form>
  );
}
