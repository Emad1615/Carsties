export { auth as proxy } from "@/auth";

export const config = {
  matcher: ["/store"],
  pages: {
    signIn: "/api/auth/signin",
  },
};
