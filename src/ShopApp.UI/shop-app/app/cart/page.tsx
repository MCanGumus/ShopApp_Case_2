"use client"; // en üstte

import React, { useEffect, useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { RootState, AppDispatch } from "@/app/store";
import { removeFromCart, clearCart } from "@/app/store/cartSlice";

export default function CartPage() {
  const dispatch = useDispatch<AppDispatch>();

  // SSR mismatch’i önlemek için geçici state
  const [mounted, setMounted] = useState(false);
  const cartItems = useSelector((state: RootState) => state.cart.items);

  useEffect(() => {
    setMounted(true);
  }, []);

  if (!mounted) return <div>Yükleniyor...</div>; // server vs client mismatch’i engeller

  if (cartItems.length === 0) {
    return (
      <div className="min-h-screen flex flex-col items-center justify-center text-gray-700">
        <p>Sepetiniz boş.</p>
      </div>
    );
  }

  const totalPrice = cartItems.reduce((sum, item) => sum + item.price, 0);

  return (
    <div className="min-h-screen bg-gray-100 p-6">
      <h1 className="text-3xl font-bold mb-6  text-gray-700">Sepetim</h1>

      <div className="space-y-4">
        {cartItems.map((item) => (
          <div key={item.id} className="bg-white p-4 rounded-2xl shadow flex justify-between items-center">
            <div>
              <h2 className="text-xl font-semibold  text-gray-700">{item.name}</h2>
              <p className=" text-gray-700">{item.price} TL</p>
            </div>
            <button
              onClick={() => dispatch(removeFromCart(item.id))}
              className="bg-red-600 text-white px-4 py-2 rounded-xl hover:bg-red-400 cursor-pointer"
            >
              Sil
            </button>
          </div>
        ))}
      </div>

      <div className="mt-6 flex justify-between items-center">
        <p className="text-xl font-bold">Toplam: {totalPrice} TL</p>
        <button
          onClick={() => dispatch(clearCart())}
          className="bg-black text-white px-6 py-3 rounded-xl"
        >
          Sepeti Temizle
        </button>
      </div>
    </div>
  );
}
