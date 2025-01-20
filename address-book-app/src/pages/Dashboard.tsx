import React, { useEffect, useState } from "react";
import {
  getAddressEntries,
  createAddressEntry,
  updateAddressEntry,
  deleteAddressEntry,
} from "../api/addressEntryApi";
import { ColumnDef } from "@tanstack/react-table";
import { Input } from '../components/ui/input';
import { DataTable } from "../components/ui/data-table";
import Modal from "../components/modal";
import { FaEdit, FaTrash, FaPlus } from "react-icons/fa";
import * as XLSX from "xlsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '../components/ui/form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm } from 'react-hook-form';
import { addressEntrySchema, AddressEntrySchema } from "../schemas/addressEntrySchema";
import DatePicker from "../components/DatePicker";
import { Controller } from "react-hook-form";
import { Button } from '../components/ui/button';
import { getDepartments } from "../api/departmentApi";
import { getJobs } from "../api/jobApi";



const Dashboard: React.FC = () => {
  const [entries, setEntries] = useState([]);
  const [loading, setLoading] = useState(false);
  const [departments, setDepartments] = useState<Department[]>([]);
const [jobs, setJobs] = useState<Job[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [pagination, setPagination] = useState({
    page: 1,
    pageSize: 5,
    totalCount: 0,
  });

  type Department = {
    id: number;
    name: string;
  };
  type Job = {
    id: number;
    title: string;
  };
  const [filters, setFilters] = useState({ search: "", sortOrder: "asc" });
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentEntry, setCurrentEntry] = useState<any | null>(null); // For editing
  const [token] = useState(() => localStorage.getItem("token") || "");

  const form = useForm<AddressEntrySchema>({
      resolver: zodResolver(addressEntrySchema),
      defaultValues: {
        email: "",
        fullName: "",
        dateOfBirth: new Date(1900, 1, 1),
        address: "",
        mobileNumber: "",
        photo: undefined,
      },
    });
  const exportToExcel = () => {
    const worksheet = XLSX.utils.json_to_sheet(entries);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, "Address Entries");
    XLSX.writeFile(workbook, "AddressEntries.xlsx");
  };

  useEffect(() => {
    const token = localStorage.getItem("token") || ""; // Get token
    setLoading(true);
    Promise.all([getDepartments(token), getJobs(token)])
      .then(([departmentsData, jobsData]) => {
        setDepartments(departmentsData);
        setJobs(jobsData);
      })
      .catch((error) => console.error("Error fetching data:", error))
      .finally(() => setLoading(false));
  }, []);

  const columns: ColumnDef<any>[] = [
    { accessorKey: "fullName", header: "Full Name" },
    { accessorKey: "email", header: "Email" },
    { accessorKey: "dateOfBirth", header: "Date of Birth" },
    { accessorKey: "address", header: "Address" },
    { accessorKey: "jobId", header: "Job ID" },
    { accessorKey: "departmentId", header: "Department ID" },
    {
      id: "actions",
      header: "Actions",
      cell: ({ row }) => {
        const entry = row.original;

        const handleEdit = () => {
          debugger;
          setCurrentEntry(entry); 
          form.reset({
            ...entry,
            departmentId: entry.departmentId.toString(), 
            jobId: entry.jobId.toString(),            
            dateOfBirth: new Date(entry.dateOfBirth),  
          });
          setIsModalOpen(true);
        };

        const handleDelete = async () => {
          if (window.confirm("Are you sure you want to delete this entry?")) {
            await deleteAddressEntry(token, entry.id);
            fetchEntries(); // Refresh data
          }
        };

        return (
          <div className="flex items-center gap-2">
            <button className="text-gray-700 hover:text-gray-900" onClick={handleEdit} title="Edit">
              <FaEdit size={16} />
            </button>
            <button
              className="text-gray-700 hover:text-gray-900"
              onClick={handleDelete}
              title="Delete"
            >
              <FaTrash size={16} />
            </button>
          </div>
        );
      },
    },
  ];

  const fetchEntries = async () => {
    try {
      setLoading(true);
      const data = await getAddressEntries(token, {
        ...filters,
        search: filters.search,
        sortOrder: filters.sortOrder,
        page: pagination.page,
        pageSize: pagination.pageSize,
      });
      setEntries(data.items);
      setPagination((prev) => ({ ...prev, totalCount: data.totalCount }));
    } catch (err: any) {
      setError(err.message || "Error fetching entries");
    } finally {
      setLoading(false);
    }
  };
  
  const handlePageChange = (newPage: number) => {
    setPagination((prev) => ({ ...prev, page: newPage }));
  };
  
  // Update entries when the page or filters change
  useEffect(() => {
    fetchEntries();
  }, [pagination.page, filters]);


  const handleSave = async (entryData: any) => {
    debugger;
    const transformedData = {
      ...entryData,
      departmentId: Number(entryData.departmentId),
      jobId: Number(entryData.jobId),
      id: currentEntry?.id,
    };
    debugger;
    try {
      if (currentEntry) {
        // Edit mode
        await updateAddressEntry(token, currentEntry.id, transformedData);
      } else {
        // Add mode
        await createAddressEntry(token, transformedData);
      }
      setIsModalOpen(false); // Close the modal
      setCurrentEntry(null); // Reset current entry
      fetchEntries(); // Refresh the table
    } catch (error) {
      console.error("Error saving entry:", error);
    }
  };
  const handleFilterChange = (key: string, value: string) => {
    setFilters((prev) => ({ ...prev, [key]: value }));
    setPagination((prev) => ({ ...prev, page: 1 })); // Reset to first page
  };
  useEffect(() => {
    fetchEntries();
  }, [pagination.page, filters]);

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-center text-2xl font-bold mb-6">Address Entries</h1>

      {/* Filters */}
      <div className="flex flex-wrap items-center justify-between mb-4 gap-4">
        <input
          type="text"
          className="input input-bordered w-full md:w-1/2 lg:w-1/3"
          placeholder="Search"
          value={filters.search}
          onChange={(e) => handleFilterChange("search", e.target.value)}
        />
        <select
          value={filters.sortOrder}
          className="select select-bordered"
          onChange={(e) => handleFilterChange("sortOrder", e.target.value)}
        >
          <option value="asc">Ascending</option>
          <option value="desc">Descending</option>
        </select>
        <button className="btn btn-primary" onClick={exportToExcel}>
          Export to Excel
        </button>
      </div>

      <button
        className="btn btn-primary mb-4 flex items-center gap-2"
        onClick={() => {
          setCurrentEntry(null); 
          form.reset();   
          setIsModalOpen(true);
        }}
      >
        <FaPlus /> Add Entry
      </button>
<DataTable
  columns={columns}
  data={entries}
  pagination={{
    page: pagination.page,
    pageSize: pagination.pageSize,
    totalCount: pagination.totalCount,
    onPageChange: handlePageChange,
  }}
/>      
      
      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title={currentEntry ? "Edit Entry" : "Add Entry"}
      >
        <Form {...form}>
        <form
          onSubmit={(e) => {
            e.preventDefault();
            const formData = new FormData(e.currentTarget as HTMLFormElement);
            const entryData = Object.fromEntries(formData.entries());
            handleSave(entryData);
          }}
        >
        <FormField
        control={form.control}
        name="fullName"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Full Name</FormLabel>
            <FormControl>
              <Input type='fullName' placeholder="Enter your Full Name" {...field} />
            </FormControl>
            {/* <FormDescription>
              This is your public display name.
            </FormDescription> */}
            <FormMessage />
          </FormItem>
        )}
      />

      <FormField
        control={form.control}
        name="address"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Address</FormLabel>
            <FormControl>
              <Input type='address' placeholder="Enter your address" {...field} />
            </FormControl>
            {/* <FormDescription>
              This is your public display name.
            </FormDescription> */}
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={form.control}
        name="email"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Email</FormLabel>
            <FormControl>
              <Input type='email' placeholder="Enter your email" {...field} />
            </FormControl>
            {/* <FormDescription>
              This is your public display name.
            </FormDescription> */}
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={form.control}
        name="mobileNumber"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Mobile Number</FormLabel>
            <FormControl>
              <Input type='phone' placeholder="Enter your mobile number" {...field} />
            </FormControl>
            {/* <FormDescription>
              This is your public display name.
            </FormDescription> */}
            <FormMessage />
          </FormItem>
        )}
      />

      <FormField
        control={form.control}
        name="dateOfBirth"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Date of Birth</FormLabel>
            <FormControl>
            <Controller
              name="dateOfBirth"
              control={form.control}
              render={({ field }) => (
                <DatePicker value={field.value} onChange={field.onChange} />
            )}
            />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
<FormField
  control={form.control}
  name="departmentId"
  render={({ field }) => (
    <FormItem>
      <FormLabel>Department</FormLabel>
      <FormControl>
        <select
          {...field}
          className="select select-bordered w-full"
          value={field.value || ""}
          onChange={(e) => field.onChange(e.target.value)}
        >
          <option value="" disabled>
            Select Department
          </option>
          {departments.map((department) => (
            <option key={department.id} value={department.id}>
              {department.name}
            </option>
          ))}
        </select>
      </FormControl>
      <FormMessage />
    </FormItem>
  )}
/>

<FormField
  control={form.control}
  name="jobId"
  render={({ field }) => (
    <FormItem>
      <FormLabel>Job</FormLabel>
      <FormControl>
        <select
          {...field}
          className="select select-bordered w-full"
          value={field.value || ""}
          onChange={(e) => field.onChange(e.target.value)}
        >
          <option value="" disabled>
            Select Job
          </option>
          {jobs.map((job) => (
            <option key={job.id} value={job.id}>
              {job.title}
            </option>
          ))}
        </select>
      </FormControl>
      <FormMessage />
    </FormItem>
  )}
/>


<FormField
control={form.control}
name="photo"
render={({ field }) => {
  const [preview, setPreview] = useState<string | null>(null);
  return(
  <FormItem>
    <FormLabel>Photo</FormLabel>
    <FormControl>
      <div className="relative">
        <label className="input input-bordered w-full flex items-center">
          <span className="mr-2">{currentEntry?.photoUrl ? "Change" : "Browse"}</span>
          <input
            type="file"
            {...form.register("photo")}
            className="absolute inset-0 opacity-0 cursor-pointer"
            onChange={(e) => {
              const file = e.target.files?.[0];
              if (file) {
                const previewUrl = URL.createObjectURL(file);
                field.onChange(file); // Update the form state with the selected file
                setPreview(previewUrl); // Set the preview URL for display
              }
            }}
          />
          {preview? (
          <div className="mt-4">
            <img
              src={preview}
              alt="Preview"
              className="w-32 h-32 object-cover border border-gray-300 rounded"
            />
          </div>
        ) : (<div className="mt-4">
          <img
            src={
              currentEntry.photoUrl.startsWith("data:image")
                ? currentEntry.photoUrl // Base64 string
                : `data:image/jpeg;base64,${currentEntry.photoUrl}` // Append base64 header
            }
            alt="Uploaded Photo"
            className="w-32 h-32 object-cover border border-gray-300 rounded"
          />
        </div>
      )}
          <span className="ml-auto text-gray-500">
            {field.value?.name || currentEntry?.fileName || "No file selected"}
          </span>
        </label>
      </div>
    </FormControl>
    
  </FormItem>
)}}
/>

      
      <Button type="submit" className='w-full'>Save</Button>
  </form>
        </Form>
      </Modal>
    </div>
  );
};

export default Dashboard;
