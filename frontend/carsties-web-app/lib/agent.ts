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
            // const errors = Object.values(data.errors).flat();
            // throw errors;
            throw JSON.stringify({
              type: data.type,
              message: data.title,
              errors: data.errors,
            });
          } else
            throw JSON.stringify({ type: "general", message: data.message });
          break;
        case 401: // Unauthorized
          throw JSON.stringify({
            type: "general",
            message: data?.detail || "Unauthorized",
          });
          break;
        case 404: // Not found
          throw JSON.stringify({ type: "general", message: "Not found" });
          break;
        case 405: // Method Not Allowed
          throw JSON.stringify({ type: "general", message: "Not Allowed" });
          break;
        case 500: //Internal server error
          throw JSON.stringify({ type: "general", message: data });
          break;
          throw JSON.stringify({ type: "general", message: statusText });
        default:
      }
    },
  );
  return agent;
};
