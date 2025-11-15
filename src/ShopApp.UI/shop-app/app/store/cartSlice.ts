"use client";

import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export interface Product {
  id: string;
  name: string;
  price: number;
  image: string;
  category: string | { name: string } | null;
}

interface CartState {
  items: Product[];
}

// localStorage’dan başlangıç
const saved = typeof window !== "undefined" ? JSON.parse(localStorage.getItem("cart") || "[]") : [];

const initialState: CartState = {
  items: saved,
};

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action: PayloadAction<Product>) => {
      if (!state.items.find((i) => i.id === action.payload.id)) {
        state.items.push(action.payload);
        localStorage.setItem("cart", JSON.stringify(state.items));
      }
    },
    removeFromCart: (state, action: PayloadAction<string>) => {
      state.items = state.items.filter((i) => i.id !== action.payload);
      localStorage.setItem("cart", JSON.stringify(state.items));
    },
    clearCart: (state) => {
      state.items = [];
      localStorage.setItem("cart", JSON.stringify(state.items));
    },
    setCart: (state, action: PayloadAction<Product[]>) => {
      // Cross-tab senkronizasyon için
      state.items = action.payload;
    },
  },
});

export const { addToCart, removeFromCart, clearCart, setCart } = cartSlice.actions;
export default cartSlice.reducer;
