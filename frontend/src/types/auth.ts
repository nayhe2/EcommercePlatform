export interface LoginAuthResponse {
    id: string;
    email: string;
    role: string;
    token: string;
}

export interface RegisterAuthResponse {
    id: string;
    email: string;
    role: string;
    token: string;
}

export interface AuthDto {
  email: string;
  password: string;
}