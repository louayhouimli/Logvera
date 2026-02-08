import { Register } from "@tanstack/react-query";
import { apiClient } from "../client";
import { AuthResponse } from "../types/auth/AuthResponse";
import { LoginRequest } from "../types/auth/LoginRequest";
import { RegisterRequest } from "../types/auth/RegisterRequest";
import { UserResponse } from "../types/auth/UserResponse";

export const register = async (
  request: RegisterRequest,
): Promise<UserResponse> => {
  const response = await apiClient.post<UserResponse>(
    "/auth/register",
    request,
  );
  return response.data;
};

export const getMe = async (): Promise<UserResponse | null> => {
  const response = await apiClient.get<UserResponse>("/auth/me");
  return response.data;
};

export const login = async (request: LoginRequest): Promise<AuthResponse> => {
  const response = await apiClient.post<AuthResponse>("/auth/login", request);
  return response.data;
};
