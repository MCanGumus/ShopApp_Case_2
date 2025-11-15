"use client";

import React, { useState } from "react";

const LoginForm = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      const res = await fetch("http://localhost:7241/api/User/Login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
      });

      if (!res.ok) {
        alert("Giriş başarısız");
        return;
      }

      const data = await res.json();

      sessionStorage.setItem("accessToken", data.accessToken);
      sessionStorage.setItem("refreshToken", data.refreshToken);

      window.location.href = "/products";

    } catch (err) {
      console.error(err);
      alert("Sunucu hatası");
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="space-y-4" onSubmit={handleSubmit}>
      <div>
        <label className="block mb-1 font-medium text-gray-700">Kullanıcı Adı</label>
        <input
          type="text"
          value={username}
          onChange={e => setUsername(e.target.value)}
          className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring-2 focus:ring-black"
          placeholder="Kullanıcı Adınızı Girin"
        />
      </div>

      <div>
        <label className="block mb-1 font-medium text-gray-700">Şifre</label>
        <input
          type="password"
          value={password}
          onChange={e => setPassword(e.target.value)}
          className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring-2 focus:ring-black"
          placeholder="Şifrenizi Girin"
        />
      </div>

      <button
        type="submit"
        disabled={loading}
        className="w-full bg-black text-white py-3 rounded-xl font-semibold hover:bg-gray-800 transition disabled:opacity-50"
      >
        {loading ? "Giriş Yapılıyor..." : "Giriş Yap"}
      </button>
    </form>
  );
};

export default LoginForm;
