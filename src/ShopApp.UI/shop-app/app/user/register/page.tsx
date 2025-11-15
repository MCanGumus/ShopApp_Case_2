import React from "react";
import RegisterForm from "@/app/components/register/RegisterForm";
import Link from "next/link";

export default function RegisterPage() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 px-4">
      <div className="w-full max-w-md bg-white-800 rounded-2xl shadow-xl p-8 space-y-6">
        <h1 className="text-3xl font-bold text-center text-gray-700">Kayıt Ol</h1>
        <RegisterForm />
        

        <p className="text-center text-sm text-gray-600">
          Hesabın var mı?
          <Link href="/user/login" className="text-black font-medium ml-1">Giriş Yap</Link>
        </p>
      </div>
    </div>
  );
}
