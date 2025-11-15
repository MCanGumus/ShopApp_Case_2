import React from "react";
import LoginForm from "@/app/components/login/LoginForm";
import Link from "next/link";

export default function LoginPage() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 px-4">
      <div className="w-full max-w-md bg-white-800 rounded-2xl shadow-xl p-8 space-y-6">
        <h1 className="text-3xl font-bold text-center text-gray-700">Giriş Yap</h1>
        <LoginForm />
        

        <p className="text-center text-sm text-gray-600">
          Hesabın yok mu?
          <Link href="/user/register" className="text-black font-medium ml-1">Kayıt Ol</Link>
        </p>
      </div>
    </div>
  );
}
