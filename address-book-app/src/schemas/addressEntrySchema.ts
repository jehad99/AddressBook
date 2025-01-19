import z  from 'zod';

export const addressEntrySchema = z.object({
    fullName: z.string().nonempty("Full Name is required"),
    email: z.string().email("Invalid email address"),
    address: z.string().nonempty("Address is required"),
    dateOfBirth: z.date().min(new Date(1900, 1, 1), "Invalid date of birth"),
    mobileNumber: z.string().nonempty("Mobile Number is required"),
    departmentId: z
    .string()
    .nonempty("Department is required")
    .transform((val) => Number(val)), // Convert string to number
  jobId: z
    .string()
    .nonempty("Job is required")
    .transform((val) => Number(val)), 
});

export type AddressEntrySchema = z.infer<typeof addressEntrySchema>;