import { getCurrentUser } from "@/app/actions/auth";
import { useQuery } from "@tanstack/react-query";

export const useCurrentUser = () => {
  const {
    data: user,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["currentUser"],
    queryFn: async () => await getCurrentUser(),
  });
  return { user, isLoading, error };
};
