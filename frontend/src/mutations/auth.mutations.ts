import { login, logout, register } from "@/api/endpoints/auth.api";
import { ErrorResponse } from "@/api/types/Error/ErrorResponse";
import { queryKeys } from "@/lib/queryKeys";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { useRouter } from "next/navigation";
import { toast } from "sonner";

export const useRegisterMutation = () => {
  const qc = useQueryClient();
  const router = useRouter();
  return useMutation({
    mutationFn: register,
    onSuccess: () => {
      toast.success("Registered successfully. Please log in.");
      router.push("/login");
    },
    onError: (error: AxiosError<ErrorResponse>) => {
      if (error.response?.data) {
        toast.error(
          error.response.data.detail ||
            "An error occurred during registration.",
        );
      }
    },
  });
};

export const useLoginMutation = () => {
  const qc = useQueryClient();
  const router = useRouter();
  return useMutation({
    mutationFn: login,
    onSuccess: () => {
      toast.success("Logged in successfully");
      qc.invalidateQueries({ queryKey: queryKeys.auth.me() });
      router.push("/dashboard");
    },
    onError: (error: AxiosError<ErrorResponse>) => {
      if (error.response?.data) {
        toast.error(
          error.response.data.detail || "An error occurred during login.",
        );
      }
    },
  });
};

export const useLogoutMutation = () => {
  const qc = useQueryClient();
  const router = useRouter();
  return useMutation({
    mutationFn: logout,
    onSuccess: () => {
      toast.success("Logged out successfully");
      router.push("/");
      qc.invalidateQueries({ queryKey: queryKeys.auth.me() });
    },
    onError: (error: AxiosError<ErrorResponse>) => {
      if (error.response?.data) {
        alert(error.response.data.detail || "An error occurred during logout.");
      }
    },
  });
};
