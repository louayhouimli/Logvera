import { getMe } from "@/api/endpoints/auth.api";
import { queryKeys } from "@/lib/queryKeys";
import { useQuery } from "@tanstack/react-query";

export const useMeQuery = () => {
  return useQuery({
    queryFn: getMe,
    queryKey: queryKeys.auth.me(),
    retry: false,
  });
};
