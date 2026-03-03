"use client";
import { NavigationBar } from "./nav/NavigationBar";

type Props = {
  children: React.ReactNode;
};
export default function Layout({ children }: Props) {
  return (
    <>
      <header>
        <NavigationBar />
      </header>
      <main className="container my-8 mx-auto text-white mt-20">
        {children}
      </main>
    </>
  );
}
