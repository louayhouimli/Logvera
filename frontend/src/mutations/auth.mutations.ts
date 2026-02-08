import { login, register } from "@/api/endpoints/auth.api";
import { queryKeys } from "@/lib/queryKeys";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useRegisterMutation = () => {
  const qc = useQueryClient();

  return useMutation({
    mutationFn: register,
    onError: (error) => {
      console.log(error);
      alert(error.message);
    },
  });
};

export const useLoginMutation = () => {
  const qc = useQueryClient();

  return useMutation({
    mutationFn: login,
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: queryKeys.auth.me() });
    },
    onError: (error) => {
      console.log(error);
      alert(error.message);
    },
  });
};
