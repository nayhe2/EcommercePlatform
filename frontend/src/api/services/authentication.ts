import axiosClient from "../axiosClient";
import type { LoginAuthResponse } from "../../types/auth";
import type { RegisterAuthResponse } from "../../types/auth";
import type { AuthDto } from "../../types/auth";

const authService = {
  login: async (credentials: AuthDto): Promise<LoginAuthResponse> => {
    const response = await axiosClient.post<LoginAuthResponse>(
      "/auth/login",
      credentials,
    );
    if (response.data.token) {
      localStorage.setItem("token", response.data.token);
    }
    return response.data;
  },
  register: async (credentials: AuthDto): Promise<RegisterAuthResponse> => {
    const response = await axiosClient.post<RegisterAuthResponse>(
      "/auth/register",
      credentials,
    );
    return response.data;
  },
};

export default authService;
