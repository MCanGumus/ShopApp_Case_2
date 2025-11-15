"use client";
import React, { useEffect, useState } from "react";
import { useParams, useRouter } from "next/navigation";
import { apiFetch } from "@/utils/api";
import Image from "next/image";
import ProductDetailActions from "@/app/components/product/ProductDetailClient";


interface Product {
  id: string;
  name: string;
  price: number;
  image: string;
  category: string | { name: string } | null;
  description?: string;
}

export default function ProductDetailPage() {
  const params = useParams();
  const router = useRouter();
  const { id } = params;
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState(true);
  

  useEffect(() => {
    const fetchProduct = async () => {
      const res = await apiFetch(`/Products/${id}`);
      if (!res) return;
      const data: Product = await res.json();
      setProduct(data);
      setLoading(false);
    };
    fetchProduct();
  }, [id]);

  if (loading) return <div>Yükleniyor...</div>;
  if (!product) return <p>Ürün bulunamadı</p>;

  const getCategoryValue = (p: Product) => {
    if (!p || p.category == null) return null;
    if (typeof p.category === "string") return p.category.trim();
    if (typeof p.category === "object" && "name" in p.category) return p.category.name.trim();
    return null;
  };

  return (

    <ProductDetailActions product={product} />

  );
}
