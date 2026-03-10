import NextAuth, { Profile } from "next-auth";
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";
import { OIDCConfig } from "next-auth/providers";

export const { handlers, signIn, signOut, auth } = NextAuth({
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
    authorized: ({ auth }) => {
      return !!auth;
    },
    jwt: ({ token, profile, account, user }) => {
      return token;
    },
    session: ({ session, token }) => {
      return session;
    },
  },
});
