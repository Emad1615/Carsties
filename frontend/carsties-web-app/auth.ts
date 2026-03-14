import NextAuth, { Profile } from "next-auth";
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";
import { OIDCConfig } from "next-auth/providers";

export const { handlers, signIn, signOut, auth } = NextAuth({
  secret: process.env.AUTH_SECRET,
  providers: [
    DuendeIDS6Provider({
      id: "id-server",
      clientId: "nextApp",
      clientSecret: "secret",
      issuer: "http://localhost:5001",
      authorization: { params: { scope: "openid profile auctionApp" } },
      idToken: true,
    } as OIDCConfig<Omit<Profile, "username">>),
  ],
  callbacks: {
    authorized: async ({ auth }) => {
      return !!auth;
    },
    jwt: async ({ token, profile, account }) => {
      if (profile) {
        token.username = profile.username;
        token.email = profile[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        ] as string | undefined;
        token.picture = profile.avatar;
      }
      if (account && account.access_token) {
        token.accessToken = account.access_token;
      }
      return token;
    },
    session: async ({ session, token }) => {
      if (token) {
        session.accessToken = token.accessToken;
        session.user = {
          ...session.user,
          id: token.sub!,
          username: token.username,
          email: token.email!,
          image: token.picture,
        };
      }
      return session;
    },
  },
  pages: {
    error: "/",
    signIn: "/api/auth/signin",
  },
});
