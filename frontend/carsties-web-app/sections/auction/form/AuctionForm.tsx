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
import { Button, Spinner } from "flowbite-react";
import { MdCancel, MdSave } from "react-icons/md";
import { useEffect } from "react";
import Image from "next/image";
import { useAddAuction } from "@/hooks/useAddAuction";
import { useUpdateAuction } from "@/hooks/useUpdateAuction";
import toast from "react-hot-toast";
import { useRouter } from "next/navigation";
type Props = {
  auction?: Auction;
};
export default function AuctionForm({ auction }: Props) {
  const {
    control,
    handleSubmit,
    formState: { isSubmitting, isValid },
    reset,
    setError,
  } = useForm<AuctionSchemaInput, FieldValues, AuctionSchemaOutput>({
    mode: "onTouched",
    resolver: zodResolver(auctionSchema),
  });
  const route = useRouter();
  const { saveAuction, isPending: isSaving } = useAddAuction();
  const { editAuction, isPending: isUpdating } = useUpdateAuction();
  const isPending = isSaving || isUpdating;
  const onHandleSubmit = async (data: FieldValues) => {
    if (auction) {
      const updatedDate = { ...data, id: auction.auctionId };
      editAuction(updatedDate, {
        onSuccess: (id) => {
          toast.success("Auction has been updated successfully");
          route.push(`/auction/details/${id}`);
        },
        //eslint-disable-next-line @typescript-eslint/no-explicit-any
        onError: (error: any) => {
          let parsedError = error;
          if (error instanceof Error) {
            try {
              parsedError = JSON.parse(error.message);
            } catch {
              parsedError = { type: "general", message: error.message };
            }
          }
          if (parsedError.type == "ValidationFailure") {
            Object.entries(parsedError.errors).forEach(([key, messages]) => {
              const field = key.charAt(0).toLowerCase() + key.slice(1);
              setError(field as keyof AuctionSchemaInput, {
                message: (messages as string[])[0],
              });
            });
          } else toast.error(parsedError.message);
        },
      });
    } else {
      saveAuction(data, {
        onSuccess: (data) => {
          toast.success("Auction has been created successfully");
          route.push(`/auction/details/${data.auctionId}`);
        },
        //eslint-disable-next-line @typescript-eslint/no-explicit-any
        onError: (error: any) => {
          let parsedError = error;
          if (error instanceof Error) {
            try {
              parsedError = JSON.parse(error.message);
            } catch {
              parsedError = { type: "general", message: error.message };
            }
          }
          if (parsedError.type == "ValidationFailure") {
            Object.entries(parsedError.errors).forEach(([key, messages]) => {
              const field = key.charAt(0).toLowerCase() + key.slice(1);
              setError(field as keyof AuctionSchemaInput, {
                message: (messages as string[])[0],
              });
            });
          } else toast.error(parsedError.message);
        },
      });
    }
  };
  useEffect(() => {
    if (auction) reset(auction);
  }, [auction, reset]);
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
      <div className="flex justify-between items-center gap-4 shadow p-3 my-2 bg-zinc-50">
        <Input
          control={control}
          id="imageUrlId"
          name="imageUrl"
          showLabel={true}
          label="Car image"
        />
        {auction && (
          <>
            <Image
              src={auction.imageUrl}
              width={"250"}
              height={"250"}
              alt={`${auction.model} car`}
              loading="lazy"
              quality={75}
              className="w-64 h-auto rounded shadow-lg hover:scale-105 duration-300 transition-all"
            />
          </>
        )}
      </div>

      <InputDatePicker
        control={control}
        id="auctionEndId"
        name="auctionEnd"
        showLabel={true}
        label="Auction end date"
      />
      <div className="flex justify-center gap-3 items-center ">
        <Button
          disabled={isSubmitting || !isValid}
          type="submit"
          outline
          color={"green"}
          size="sm"
          className="flex items-center justify-center gap-2 pointer-cursor px-10 uppercase transition-all duration-200 hover:scale-110"
        >
          {isSubmitting || isPending ? (
            <>
              <Spinner color="success" /> Loading....
            </>
          ) : (
            <>
              <MdSave /> Add
            </>
          )}
        </Button>
        <Button
          type="reset"
          onClick={() => reset()}
          outline
          color={"red"}
          size="sm"
          className="flex items-center justify-center gap-2 pointer-cursor px-10 uppercase transition-all duration-200 hover:scale-110"
        >
          <MdCancel />
          Cancel
        </Button>
      </div>
    </form>
  );
}
