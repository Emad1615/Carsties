"use client";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { NavigationBar } from "./nav/NavigationBar";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { Suspense } from "react";
import Loading from "./components/Loading";
import { Toaster } from "react-hot-toast";

type Props = {
  children: React.ReactNode;
};
export default function Layout({ children }: Props) {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: {
        staleTime: 0,
      },
    },
  });
  return (
    <Suspense fallback={<Loading />}>
      <QueryClientProvider client={queryClient}>
        <ReactQueryDevtools initialIsOpen={false} />
        <header>
          <NavigationBar />
        </header>
        <main className="container my-8 mx-auto text-white mt-20  ">
          {children}
        </main>
      </QueryClientProvider>
      <Toaster
        position="top-right"
        reverseOrder={false}
        gutter={12}
        toasterId="default"
        toastOptions={{
          className: "min-w-50",
          duration: 5000,
          removeDelay: 1000,
          style: {
            background: "#363636",
            color: "#fff",
          },
          success: {
            duration: 3000,
            style: {
              background: "white",
              color: "black",
              fontSize: "14px",
            },
            iconTheme: {
              primary: "green",
              secondary: "white",
            },
          },
          error: {
            duration: 3000,
            style: {
              background: "white",
              color: "black",
              fontSize: "13px",
              fontWeight: "500",
            },
            iconTheme: {
              primary: "red",
              secondary: "white",
            },
          },
        }}
      />
    </Suspense>
  );
}
