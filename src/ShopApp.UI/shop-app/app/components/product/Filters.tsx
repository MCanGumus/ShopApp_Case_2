"use client";
import React from "react";


interface CategoryOption {
    key: string;
    display: string;
}


interface Props {
    categories: CategoryOption[];
    selectedCategory: string;
    setSelectedCategory: (v: string) => void;
    minPrice: string;
    setMinPrice: (v: string) => void;
    maxPrice: string;
    setMaxPrice: (v: string) => void;
    sortBy: string;
    setSortBy: (v: string) => void;
}


export default function Filters({
    categories,
    selectedCategory,
    setSelectedCategory,
    minPrice,
    setMinPrice,
    maxPrice,
    setMaxPrice,
    sortBy,
    setSortBy,
}: Props) {
    return (
        <div className="bg-white p-4 rounded-2xl shadow mb-6 flex flex-wrap gap-4 items-center">
            <select
                className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring-2 focus:ring-black"
                value={selectedCategory}
                onChange={(e) => setSelectedCategory(e.target.value)}
            >
                {categories.map((c) => (
                    <option key={c.key} value={c.key}>
                        {c.display}
                    </option>
                ))}
            </select>


            <input
                type="number"
                placeholder="Min Fiyat"
                className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring-2 focus:ring-black"
                value={minPrice}
                onChange={(e) => setMinPrice(e.target.value)}
            />


            <input
                type="number"
                placeholder="Max Fiyat"
                className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring-2 focus:ring-black"
                value={maxPrice}
                onChange={(e) => setMaxPrice(e.target.value)}
            />


            <select
                className="w-full px-4 py-2 border rounded-xl border-gray-700 text-gray-700 focus:outline-none focus:ring-2 focus:ring-black"
                value={sortBy}
                onChange={(e) => setSortBy(e.target.value)}
            >
                <option value="">Sırala</option>
                <option value="price_asc">Fiyat (Artan)</option>
                <option value="price_desc">Fiyat (Azalan)</option>
            </select>
        </div>
    );
}
