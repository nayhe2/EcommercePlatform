import axios from "axios";
import type { AxiosInstance } from "axios";

const axiosClient: AxiosInstance = axios.create({
  baseURL: "https://localhost:7142/api",
  headers: {
    "Content-Type": "application/json",
  },
});

export default axiosClient;
