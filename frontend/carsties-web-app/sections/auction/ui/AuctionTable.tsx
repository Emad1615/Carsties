import { Table, TableBody, TableCell, TableRow } from "flowbite-react";
type Props = {
  auction?: Auction;
};
export default function AuctionTable({ auction }: Props) {
  const { make, model, color, year, mileage } = auction || {};
  return (
    <div className="shadow  border border-zinc-50  p-2 overflow-x-auto">
      <h4 className="text-lg font-medium text-gray-500 uppercase py-2 px-1  mb-2">
        Car information
      </h4>
      <Table striped hoverable className="text-sm text-left text-gray-500 ">
        <TableBody className="divide-y divide-gray-300 ">
          <TableRow>
            <TableCell className="whitespace-nowrap font-medium text-gray-900 ">
              Make
            </TableCell>
            <TableCell>{make}</TableCell>
            <TableCell className="whitespace-nowrap font-medium text-gray-900 ">
              Model
            </TableCell>
            <TableCell>{model}</TableCell>
          </TableRow>
          <TableRow>
            <TableCell className="whitespace-nowrap font-medium text-gray-900 ">
              Color
            </TableCell>
            <TableCell>{color}</TableCell>
            <TableCell className="whitespace-nowrap font-medium text-gray-900 ">
              Year
            </TableCell>
            <TableCell>{year}</TableCell>
          </TableRow>
          <TableRow>
            <TableCell className="whitespace-nowrap font-medium text-gray-900 ">
              Milage
            </TableCell>
            <TableCell>{mileage}</TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </div>
  );
}
