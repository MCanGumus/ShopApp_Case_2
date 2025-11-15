"use client";

import React, { useEffect, useMemo, useState } from "react";
import { apiFetch } from "@/utils/api";
import ProductCard from "@/app/components/product/ProductCard";
import Filters from "@/app/components/product/Filters";
import { useSelector } from "react-redux";
import { RootState } from "@/app/store";
import Link from "next/link";

interface Product {
  id: string;
  name: string;
  price: number;
  imageUrl: string;
  category: string | { name: string } | null;
}

interface CategoryOption {
  key: string;
  display: string;
}


export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedCategory, setSelectedCategory] = useState<string>("all");
  const [minPrice, setMinPrice] = useState("");
  const [maxPrice, setMaxPrice] = useState("");
  const [sortBy, setSortBy] = useState("");
  const cartItems = useSelector((state: RootState) => state.cart.items);

  const getCategoryValue = (p: Product): string | null => {
    if (!p || p.category == null) return null;
    if (typeof p.category === "string") return p.category.trim();
    if (typeof p.category === "object" && "name" in p.category) return p.category.name.trim();
    return null;
  };

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const res = await apiFetch("/Products", { method: "GET" });
        if (!res) return;
        const data = await res.json();
        const normalized: Product[] = (data || []).map((d: any) => ({
          id: d.id,
          name: d.name,
          price: Number(d.price ?? 0),
          image: d.image ?? "",
          category: d.category ?? d.categoryName ?? null,
        }));
        setProducts(normalized);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    fetchProducts();
  }, []);

  const categories: CategoryOption[] = useMemo(() => {
    const map = new Map<string, string>();
    products.forEach((p) => {
      const raw = getCategoryValue(p);
      if (!raw) return;
      const normalized = raw.toLowerCase();
      if (!map.has(normalized)) map.set(normalized, raw);
    });
    return [{ key: "all", display: "Tümü" }, ...Array.from(map.entries()).map(([key, display]) => ({ key, display }))];
  }, [products]);

  const filteredProducts = useMemo(() => {
    let result = [...products];
    if (selectedCategory !== "all") result = result.filter((p) => getCategoryValue(p)?.toLowerCase() === selectedCategory);
    if (minPrice) result = result.filter((p) => p.price >= Number(minPrice));
    if (maxPrice) result = result.filter((p) => p.price <= Number(maxPrice));
    if (sortBy === "price_asc") result.sort((a, b) => a.price - b.price);
    if (sortBy === "price_desc") result.sort((a, b) => b.price - a.price);
    return result;
  }, [products, selectedCategory, minPrice, maxPrice, sortBy]);

  if (loading) return <div className="min-h-screen flex items-center justify-center">Yükleniyor...</div>;

  return (
    <div className="min-h-screen bg-gray-100 p-6">
      <h1 className="text-3xl font-bold mb-6 text-gray-800">Ürünler</h1>
      <Filters
        categories={categories}
        selectedCategory={selectedCategory}
        setSelectedCategory={setSelectedCategory}
        minPrice={minPrice}
        setMinPrice={setMinPrice}
        maxPrice={maxPrice}
        setMaxPrice={setMaxPrice}
        sortBy={sortBy}
        setSortBy={setSortBy}
      />
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {filteredProducts.map((product) => (
          <ProductCard key={product.id} product={product} getCategoryValue={getCategoryValue} />
        ))}
      </div>
      {cartItems.length > 0 && (
        <Link href={"/cart"} className="fixed bottom-6 right-6 bg-black text-white px-6 py-3 rounded-xl shadow-lg">
          Sepette {cartItems.length} ürün var
        </Link>
      )}
    </div>
  );
}
