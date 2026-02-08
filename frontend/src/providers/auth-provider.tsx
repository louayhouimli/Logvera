"use client";
import { UserResponse } from "@/api/types/auth/UserResponse";
import { useMeQuery } from "@/queries/auth.queries";
import { createContext, useMemo } from "react";

type AuthContextValue = {
  user: UserResponse | null;
  isAuthenticated: boolean;
  isLoading: boolean;
};

export const AuthContext = createContext<AuthContextValue | undefined>(
  undefined,
);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const { data, isLoading, isError } = useMeQuery();
  const value = useMemo<AuthContextValue>(() => {
    const user = isError ? null : (data ?? null);
    return {
      user,
      isAuthenticated: Boolean(user),
      isLoading,
    };
  }, [data, isLoading, isError]);

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
