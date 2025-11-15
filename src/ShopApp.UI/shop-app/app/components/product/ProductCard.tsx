"use client";

import React from "react";
import Link from "next/link";
import { useDispatch } from "react-redux";
import { AppDispatch } from "@/app/store";
import { addToCart as addToCartAction } from "@/app/store/cartSlice";
import Image from "next/image";

interface Product {
  id: string;
  name: string;
  price: number;
  image: string;
  category: string | { name: string } | null;
}

interface Props {
  product: Product;
  getCategoryValue: (p: Product) => string | null;
}

export default function ProductCard({ product, getCategoryValue }: Props) {
  const dispatch = useDispatch<AppDispatch>();
  const handleAddToCart = () => dispatch(addToCartAction(product));

  return (
    <div className="bg-white rounded-2xl shadow-md p-4 hover:shadow-lg transition">
      <Link href={`/products/${product.id}`}>
        <Image
          src={product.image || "/next.svg"}
          alt={product.name}
          width={500}
          height={500}
          className="w-full h-40 object-cover rounded-xl mb-4"
          priority={false}
        />
        <h2 className="text-xl font-semibold text-gray-800">{product.name}</h2>
        <p className="text-gray-600 text-sm mb-1">{getCategoryValue(product) ?? "Kategori Yok"}</p>
        <p className="text-gray-600 text-lg mb-3">{product.price} TL</p>
      </Link>
      <button onClick={handleAddToCart} className="w-full bg-black text-white py-2 rounded-xl font-medium hover:bg-gray-800 transition">
        Sepete Ekle
      </button>
    </div>
  );
}
