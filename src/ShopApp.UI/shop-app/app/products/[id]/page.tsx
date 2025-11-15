
import ProductDetailClient from "@/app/components/product/ProductDetailClient";

interface Product {
  id: string;
  name: string;
  price: number;
  imageUrl: string;
  category: string | { name: string } | null;
  description: string;
}

export async function generateMetadata({ params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  const res = await fetch(`http://localhost:7072/api/Products/${id}`, {
    cache: 'no-store'
  });
  if (!res.ok) return { title: "Ürün bulunamadı", description: "" };
  const product: Product = await res.json();
  return {
    title: product.name,
    description: product.description || `${product.name} ürününü inceleyin.`,
  };
}

export default async function ProductDetailPage({ params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  const res = await fetch(`http://localhost:7072/api/Products/${id}`, {
    cache: 'no-store'
  });

  if (!res.ok) return <p>Ürün bulunamadı</p>;
  const product: Product = await res.json();
  return <ProductDetailClient product={product} />;
}
