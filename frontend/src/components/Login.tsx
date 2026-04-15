import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import authService from "../api/services/authentication";
import {
  ScanFace,
  Mail,
  Lock,
  ArrowRight,
  Loader2,
  AlertCircle,
} from "lucide-react";
import "../App.css";

function Login({ onLoginSuccess }: { onLoginSuccess: () => void }) {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setIsLoading(true);
    setErrorMessage(null);

    try {
      await authService.login({ email, password });
      console.log("Login success");
      onLoginSuccess();
      navigate("/home");
    } catch (error: any) {
      console.error("Login error:", error.message);
      setErrorMessage(error.message || "Invalid email or password.");
      setPassword("");
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col items-center justify-center p-6">
      <div className="w-full max-w-md bg-white rounded-4xl shadow-xl shadow-gray-200/50 border border-gray-100 p-8 lg:p-10">
        {/* header */}
        <div className="flex flex-col items-center mb-8">
          <div className="w-16 h-16 bg-gray-50 rounded-2xl flex items-center justify-center mb-4 text-gray-900 border border-gray-100">
            <ScanFace className="w-8 h-8" />
          </div>
          <h2 className="text-2xl font-bold text-gray-900 tracking-tight">
            Welcome Back
          </h2>
          <p className="text-sm text-gray-400 mt-2">Enter your credentials</p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-5">
          {errorMessage && (
            <div className="bg-red-50 border border-red-100 text-red-600 p-4 rounded-xl flex items-start gap-3 text-sm animate-in fade-in slide-in-from-top-2">
              <AlertCircle className="w-5 h-5 shrink-0 mt-0.5" />
              <span>{errorMessage}</span>
            </div>
          )}

          <div className="space-y-1">
            <label
              className="text-xs font-semibold text-gray-500 uppercase tracking-wide ml-1"
              htmlFor="email"
            >
              Email Address
            </label>
            <div className="relative group">
              <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-gray-400 group-focus-within:text-gray-800 transition-colors">
                <Mail className="w-5 h-5" />
              </div>
              <input
                className="w-full pl-11 pr-4 py-3.5 bg-gray-50 border border-gray-200 text-gray-900 rounded-xl focus:outline-none focus:ring-2 focus:ring-black/5 focus:border-gray-400 transition-all font-medium placeholder:text-gray-400"
                type="email"
                id="email"
                placeholder="your.name@email.com"
                value={email}
                onChange={(e) => {
                  setEmail(e.target.value);
                  if (errorMessage) setErrorMessage(null);
                }}
                required
              />
            </div>
          </div>

          <div className="space-y-1">
            <label
              className="text-xs font-semibold text-gray-500 uppercase tracking-wide ml-1"
              htmlFor="password"
            >
              Password
            </label>
            <div className="relative group">
              <div className="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none text-gray-400 group-focus-within:text-gray-800 transition-colors">
                <Lock className="w-5 h-5" />
              </div>
              <input
                className="w-full pl-11 pr-4 py-3.5 bg-gray-50 border border-gray-200 text-gray-900 rounded-xl focus:outline-none focus:ring-2 focus:ring-black/5 focus:border-gray-400 transition-all font-medium placeholder:text-gray-400"
                type="password"
                id="password"
                placeholder="••••••••"
                value={password}
                onChange={(e) => {
                  setPassword(e.target.value);
                  if (errorMessage) setErrorMessage(null);
                }}
                required
              />
            </div>
          </div>

          <button
            type="submit"
            disabled={isLoading}
            className="w-full mt-4 py-4 rounded-xl font-semibold bg-black text-white shadow-lg shadow-black/20 hover:bg-gray-800 transition-all active:scale-[0.98] flex items-center justify-center gap-2 disabled:opacity-70 disabled:cursor-not-allowed"
          >
            {isLoading ? (
              <Loader2 className="w-5 h-5 animate-spin" />
            ) : (
              <>
                Sign In <ArrowRight className="w-4 h-4" />
              </>
            )}
          </button>
        </form>

        <div className="mt-8 pt-6 border-t border-gray-100 text-center">
          <p className="text-sm text-gray-500">
            Don't have an account?{" "}
            <button
              onClick={() => navigate("/register")}
              className="font-semibold text-gray-900 hover:text-black hover:underline transition-all"
            >
              Register now
            </button>
          </p>
        </div>
      </div>
    </div>
  );
}

export default Login;
