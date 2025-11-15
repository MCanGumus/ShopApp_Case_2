
"use client";

import React from "react";
import Image from "next/image";
import { useRouter } from "next/navigation";
import { useDispatch } from "react-redux";
import { addToCart as addToCartAction } from "@/app/store/cartSlice";
import { AppDispatch } from "@/app/store";
import { useSelector } from "react-redux";
import { RootState } from "@/app/store";
import Link from "next/link";

interface Product {
  id: string;
  name: string;
  price: number;
  imageUrl: string;
  category: string | { name: string } | null;
  description: string;
}

interface Props {
  product: Product;
}

export default function ProductDetailClient({ product }: Props) {
  const router = useRouter();
  const dispatch = useDispatch<AppDispatch>();
  const cartItems = useSelector((state: RootState) => state.cart.items);

  const getCategoryValue = (p: Product) => {
    if (!p || p.category == null) return null;
    if (typeof p.category === "string") return p.category.trim();
    if (typeof p.category === "object" && "name" in p.category) return p.category.name.trim();
    return null;
  };

  const handleAddToCart = () => {
    dispatch(addToCartAction(product));
  };

  return (
    <div className="min-h-screen bg-gray-100 p-6">
      <button
        onClick={() => router.push("/products")}
        className="mb-6 px-4 py-2 bg-black text-white rounded-xl"
      >
        Geri Dön
      </button>

      <div className="bg-white p-6 rounded-2xl shadow-lg max-w-4xl mx-auto flex flex-col lg:flex-row gap-6">
        <Image
          src={product.imageUrl || "/next.svg"}
          alt={product.name}
          width={500}
          height={500}
          className="w-full lg:w-1/2 h-80 object-cover rounded-xl"
        />
        <div className="flex-1 flex flex-col justify-between">
          <div>
            <h1 className="text-3xl font-bold mb-2 text-gray-900">{product.name}</h1>
            <p className="text-gray-600 mb-2">{getCategoryValue(product) || "Kategori Yok"}</p>
            <p className="text-gray-800 text-xl font-semibold mb-4">{product.price} TL</p>
            <p className="text-gray-700">{product.description || "Ürün açıklaması yok."}</p>
          </div>
          <button
            onClick={handleAddToCart}
            className="mt-6 w-full lg:w-auto bg-black text-white py-2 px-6 rounded-xl font-medium hover:bg-gray-800 transition"
          >
            Sepete Ekle
          </button>
        </div>
      </div>
      {cartItems.length > 0 && (
        <Link href={"/cart"} className="fixed bottom-6 right-6 bg-black text-white px-6 py-3 rounded-xl shadow-lg">
          Sepette {cartItems.length} ürün var
        </Link>
      )}
    </div>
  );
}
