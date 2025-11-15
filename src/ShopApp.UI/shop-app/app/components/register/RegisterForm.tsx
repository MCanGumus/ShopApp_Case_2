'use client';

import React, { useState } from "react";

const RegisterForm = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [passwordCheck, setPasswordCheck] = useState("");
  const [email, setEmail] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    if (password !== passwordCheck) {
      alert("Şifreler eşleşmiyor");
      setLoading(false);
      return;
    }

    try {
      const res = await fetch("http://localhost:7241/api/User/Register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password, email }), 
      });

      if (!res.ok) {
        alert("Kayıt başarısız");
        return;
      }
      alert("Kayıt başarılı. Giriş sayfasına yönlendiriliyorsunuz.");
      
      window.location.href = "/user/login";

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
          onChange={e => setUsername(e.target.value)}
          className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring focus:ring-gray-300"
          placeholder="Kullanıcı Adınızı Girin"
        />
      </div>

      <div>
        <label className="block mb-1 font-medium text-gray-700">Email</label>
        <input
          type="email"
          onChange={e => setEmail(e.target.value)}
          className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring focus:ring-gray-300"
          placeholder="example@mail.com"
        />
      </div>

      <div>
        <label className="block mb-1 font-medium text-gray-700">Şifre</label>
        <input
          type="password"
          onChange={e => setPassword(e.target.value)}
          className="w-full px-4 py-2 border rounded-xl  border-gray-700 text-gray-700 focus:outline-none focus:ring focus:ring-gray-300"
          placeholder="Şifrenizi Girin"
        />
      </div>

      <div>
        <label className="block mb-1 font-medium text-gray-700">Şifre Tekrar</label>
        <input
          type="password"
          onChange={e => setPasswordCheck(e.target.value)}
          className="w-full px-4 py-2 border rounded-xl  border-gray-700 text-gray-700 focus:outline-none focus:ring focus:ring-gray-300"
          placeholder="Şifrenizi Tekrar Girin"
        />
      </div>

      <button
        type="submit"
        disabled={loading}
        className="w-full bg-black text-white py-3 rounded-xl font-semibold hover:bg-gray-800 transition disabled:opacity-50"
      >
        {loading ? "Kayıt Yapılıyor..." : "Kayıt Ol"}
      </button>
    </form>
  );
}

export default RegisterForm;