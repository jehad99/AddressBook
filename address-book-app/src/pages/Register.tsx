import React from 'react';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage, FormDescription } from '../components/ui/form';
import { Button } from '../components/ui/button';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm } from 'react-hook-form';
import { Input } from '../components/ui/input';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { registerSchema, RegisterSchema } from "../schemas/registerSchema";

const Register: React.FC = () => {
  const navigate = useNavigate();

  const form = useForm<RegisterSchema>({
    resolver: zodResolver(registerSchema),
    defaultValues: {
      email: "",
      password: "",
      fullName: "",
    },
  });  
  const onSubmit = async (data: RegisterSchema) => {
    try {
      const response = await axios.post("https://localhost:44349/api/Auth/register", data);
      const { token } = response.data;
debugger;
      localStorage.setItem("token", token);

      navigate("/dashboard");
    } catch (error) {
      form.setError("password", { message: "Invalid email or password" });
    }
  };
  return(
    <div className="flex items-center justify-center h-screen bg-gray-100">
    <Form {...form}>
    <form onSubmit={form.handleSubmit(onSubmit)} className="w-full max-w-md p-6 space-y-4 bg-white rounded-md shadow">
    <h1 className="text-2xl font-semibold text-center">Register</h1>
    <FormField
        control={form.control}
        name="fullName"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Full Name</FormLabel>
            <FormControl>
              <Input type='fullName' placeholder="Enter your full name" {...field} />
            </FormControl>
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
        name="password"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Password</FormLabel>
            <FormControl>
              <Input type='password' placeholder="Enter your password" {...field} />
            </FormControl>
            <FormMessage />
          </FormItem>
          
        )}
      />
      <Button type="submit" className='w-full'>Register</Button>
      <p
        className="mt-4 text-sm text-center text-blue-500 cursor-pointer"
        onClick={() => navigate("/")}
      >
        Do you have an account? Go to Login
      </p>
    </form>
  </Form>
  </div>
  )
}
export default Register;