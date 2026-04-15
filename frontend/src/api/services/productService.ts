import axiosClient from "../axiosClient";

export const productService = {
  getAll: () => axiosClient.get("/products"),
  getById: (id: number) => axiosClient.get(`/products/${id}`),
  create: (data: any) => axiosClient.post("/products", data),
};
