import axios from "axios";

const BASE_URL = "https://localhost:44349/api/AddressEntries";

export const getAddressEntries = async (token: string, params: Record<string, any>) => {
  try {
    debugger;
    const response = await axios.get(BASE_URL, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      params, 
    });
    return response.data; 
  } catch (error) {
    console.error("Error fetching address entries:", error);
    throw error;
  }
};

export const createAddressEntry = async (token: string, data: Record<string, any>) => {
  try {
    const response = await axios.post(BASE_URL, data, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error creating address entry:", error);
    throw error;
  }
};

export const updateAddressEntry = async (token: string, id: string, data: Record<string, any>) => {
  try {
    const response = await axios.put(`${BASE_URL}/${id}`, data, {
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error updating address entry:", error);
    throw error;
  }
}

export const deleteAddressEntry = async (token: string, id: string) => {
  try {
    const response = await axios.delete(`${BASE_URL}/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error deleting address entry:", error);
    throw error;
  }
}