"use client";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { NavigationBar } from "./nav/NavigationBar";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { Suspense } from "react";
import Loading from "./components/Loading";

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
        <main className="container my-8 mx-auto text-white mt-20">
          {children}
        </main>
      </QueryClientProvider>
    </Suspense>
  );
}
