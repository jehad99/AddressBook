import axios from "axios";

const baseUrl = "https://localhost:44349/api/Departments";

export const getDepartments = async (token: string) => {
    const response = await axios.get(baseUrl, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  };

