import z from 'zod';

export const registerSchema = z.object({
    email: z.string().email("Invalid email address").nonempty("Email is required"),
    password: z.string().min(6, "Password must be at least 6 characters").nonempty("Password is required"),
    fullName: z.string().nonempty("Full name is required"),
});

export type RegisterSchema = z.infer<typeof registerSchema>;