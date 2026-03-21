import { URL } from "url";
import * as z from "zod";

export const auctionSchema = z.object({
  reservePrice: z.coerce
    .number()
    .refine((value) => !isNaN(value), { message: "Reserve price is required" })
    .min(0, { message: "reservePrice must be at least 0" }),

  auctionEnd: z.coerce
    .date({ message: "End date is required" })
    .refine((date) => date > new Date(), {
      message: "End date must be in the future",
    }),

  color: z.string().min(1, { message: "Color is required" }),
  make: z.string().min(1, { message: "Make is required" }),
  model: z.string().min(1, { message: "Model is required" }),

  year: z.coerce
    .number()
    .refine((value) => !isNaN(value), { message: 'Year is required"' })
    .min(1970, { message: "Year must be greater than 1970" })
    .max(new Date().getFullYear(), {
      message: `Year must be less than ${new Date().getFullYear()}`,
    }),

  mileage: z.coerce
    .number()
    .min(0, { message: "Mileage must be at least 0" })
    .max(1000000, { message: "Mileage must be less than 1,000,000" }),

  imageUrl: z
    .string()
    .refine((value) => URL.canParse(value), { message: "Invalid image URL" }),
});

export type AuctionSchema = z.infer<typeof auctionSchema>;

export type AuctionSchemaInput = z.input<typeof auctionSchema>;
export type AuctionSchemaOutput = z.output<typeof auctionSchema>;
