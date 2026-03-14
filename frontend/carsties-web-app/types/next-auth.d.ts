import { type DefaultSession } from "next-auth";
//eslint-disable-next-line
import { JWT } from "next-auth/jwt";

declare module "next-auth" {
  /**
   * Returned by `auth`, `useSession`, `getSession` and received as a prop on the `SessionProvider` React Context
   */
  interface Session {
    user: {
      username: string;
    } & DefaultSession["user"];
    accessToken: string;
  }
  interface Profile {
    username: string;
    avatar: string;
  }
  interface User {
    username: string;
  }
}

// The `JWT` interface can be found in the `next-auth/jwt` submodule

declare module "next-auth/jwt" {
  /** Returned by the `jwt` callback and `auth`, when using JWT sessions */
  interface JWT {
    /** OpenID ID Token */
    idToken?: string;
    username: string;
    accessToken: string;
    avatar: string;
  }
}
