import { useContext } from "react";
import { AuthContext } from "@/providers/auth-provider";
import { useQuery, useQueryClient } from "@tanstack/react-query";
import { getMe } from "@/api/endpoints/auth.api";
import { queryKeys } from "@/lib/queryKeys";

export const useAuth = () => {
  const { data: user, isLoading } = useQuery({
    queryFn: getMe,
    queryKey: queryKeys.auth.me(),
  });

  return {
    user: user ?? null,
    isLoading,
  };
};
