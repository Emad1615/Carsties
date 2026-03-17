import { auth } from "@/auth";
import axios from "axios";

export const createAgent = async () => {
  const agent = axios.create({
    baseURL: process.env.NEXT_PUBLIC_API_URL,
    headers: {
      "Content-Type": "application/json",
    },
    withCredentials: true,
  });

  agent.interceptors.request.use(async (config) => {
    const session = await auth();
    if (session?.accessToken)
      config.headers.Authorization = `Bearer ${session?.accessToken}`;
    return config;
  });

  agent.interceptors.response.use(
    async (response) => {
      return response;
    },
    async (error) => {
      const { data, status, statusText } = error.response;
      switch (status) {
        case 400: // Bad request
          if (data.errors) {
            const errors = Object.values(data.errors).flat();
            return Promise.resolve(errors);
          } else return Promise.reject(data);
          break;
        case 401: // Unauthorized
          return Promise.reject(new Error(data?.detail || "Unauthorized"));
          break;
        case 404: // Not found
          return Promise.reject(new Error("Not Found"));
          break;
        case 405: // Method Not Allowed
          return Promise.reject(new Error(statusText || "Not Allowed"));
          break;
        case 500: //Internal server error
          return Promise.reject(data);
          break;
          return Promise.reject(new Error(data?.title ?? statusText));
        default:
      }
    },
  );
  return agent;
};
