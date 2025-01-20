"use client"

import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  useReactTable,
  getPaginationRowModel,
} from "@tanstack/react-table"
import { Button } from "../ui/button"

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
  
} from "./table"

interface DataTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[]
  data: TData[] 
  pagination: {
    page: number;
    pageSize: number;
    totalCount: number;
    onPageChange: (newPage: number) => void;
  };
}


export function DataTable<TData, TValue>({
  columns,
  data,
  pagination,
}: DataTableProps<TData, TValue>) {
  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true, 
    pageCount: Math.ceil(pagination.totalCount / pagination.pageSize),
    state: {
      pagination: { pageIndex: pagination.page - 1, pageSize: pagination.pageSize },
    },
    onPaginationChange: (updater) => {
      const newPageIndex =
        typeof updater === "function" ? updater(table.getState().pagination).pageIndex : updater.pageIndex;

      pagination.onPageChange(newPageIndex + 1); // Adjust back to 1-based index
    },
    });
  return (
    <div>
    <div className="rounded-md border">
      <Table>
        <TableHeader>
          {table.getHeaderGroups().map((headerGroup) => (
            <TableRow key={headerGroup.id}>
              {headerGroup.headers.map((header) => {
                return (
                  <TableHead key={header.id}>
                    {header.isPlaceholder
                      ? null
                      : flexRender(
                          header.column.columnDef.header,
                          header.getContext()
                        )}
                  </TableHead>
                )
              })}
            </TableRow>
          ))}
        </TableHeader>
        <TableBody>
          {table.getRowModel().rows?.length ? (
            table.getRowModel().rows.map((row) => (
              <TableRow
                key={row.id}
                data-state={row.getIsSelected() && "selected"}
              >
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))
          ) : (
            <TableRow>
              <TableCell colSpan={columns.length} className="h-24 text-center">
                No results.
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </div>
    <div className="flex items-center justify-between mt-4">
        <Button
          variant="outline"
          size="sm"
          onClick={() => pagination.onPageChange(pagination.page - 1)}
          disabled={pagination.page === 1}
        >
          Previous
        </Button>
        <span>
          Page {pagination.page} of{" "}
          {Math.ceil(pagination.totalCount / pagination.pageSize)}
        </span>
        <Button
          variant="outline"
          size="sm"
          onClick={() => pagination.onPageChange(pagination.page + 1)}
          disabled={
            pagination.page >=
            Math.ceil(pagination.totalCount / pagination.pageSize)
          }
        >
          Next
        </Button>
      </div>
            </div>
  
  )
}
