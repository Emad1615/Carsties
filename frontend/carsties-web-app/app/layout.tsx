import type { Metadata } from "next";
import { Geist } from "next/font/google";
import "./globals.css";
import Layout from "@/layouts/Layout";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: {
    default: "Carsties ",
    template: "%s | Carsties ",
  },
  description: "Auctions for cars and car parts ",
  icons: {
    icon: "/icon.png",
  },
};

export default async function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" suppressHydrationWarning={true}>
      <body className={`${geistSans.variable}  antialiased h-dvh `}>
        <Layout>{children}</Layout>
      </body>
    </html>
  );
}
