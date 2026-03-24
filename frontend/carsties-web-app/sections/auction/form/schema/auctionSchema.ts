import { URL } from "url";
import * as z from "zod";

export const auctionSchema = z.object({
  reservePrice: z.coerce
    .number({ error: "Reservice price is required" })
    .refine((value) => !isNaN(value), { error: "Reserve price is required" })
    .min(0, { error: "reservePrice must be at least 0" }),

  auctionEnd: z.coerce
    .date({ error: "End date is required" })
    .refine((date) => date > new Date(), {
      error: "End date must be in the future",
    }),

  color: z
    .string({ error: "Color is required" })
    .min(1, { error: "Color is required" }),
  make: z
    .string({ error: "Make is required" })
    .min(1, { error: "Make is required" }),
  model: z
    .string({ error: "Model is required" })
    .min(1, { error: "Model is required" }),

  year: z.coerce
    .number({ error: "number is required" })
    .refine((value) => !isNaN(value), { error: 'Year is required"' })
    .min(1970, { error: "Year must be greater than 1970" })
    .max(new Date().getFullYear(), {
      error: `Year must be less than ${new Date().getFullYear()}`,
    }),

  mileage: z.coerce
    .number({ error: "Milage is required" })
    .min(0, { error: "Mileage must be at least 0" })
    .max(1000000, { error: "Mileage must be less than 1,000,000" }),

  imageUrl: z.url({ error: "Invalid image URL" }),
});

export type AuctionSchema = z.infer<typeof auctionSchema>;

export type AuctionSchemaInput = z.input<typeof auctionSchema>;
export type AuctionSchemaOutput = z.output<typeof auctionSchema>;
