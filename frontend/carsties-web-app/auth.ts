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
    } as OIDCConfig<Profile>),
  ],
  callbacks: {
    authorized: async ({ auth }) => {
      return !!auth;
    },
    jwt: async ({ token, profile, account, user }) => {
      console.log("token :", token);
      console.log("profile :", profile);
      console.log("account :", account);
      console.log("user :", user);
      return token;
    },
    session: async ({ session, token }) => {
      if (token) {
        session.user.id = token.sub!;
      }
      console.log("session :", session.user);
      return session;
    },
  },
});
