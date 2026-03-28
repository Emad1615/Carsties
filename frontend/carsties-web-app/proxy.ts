export { auth as proxy } from "@/auth";

export const config = {
  matcher: ["/store", "/auction/:path*"],
  pages: {
    signIn: "/api/auth/signin",
  },
};
