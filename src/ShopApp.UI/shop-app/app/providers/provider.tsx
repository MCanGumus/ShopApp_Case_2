"use client";

import { Provider, useDispatch } from "react-redux";
import { store, AppDispatch } from "@/app/store";
import { useEffect } from "react";
import { setCart } from "@/app/store/cartSlice";

interface Props {
  children: React.ReactNode;
}

export default function Providers({ children }: Props) {
  const dispatch: AppDispatch = store.dispatch;

  useEffect(() => {
    // Cross-tab senkronizasyon
    const handleStorage = (e: StorageEvent) => {
      if (e.key === "cart") {
        const newCart = JSON.parse(e.newValue || "[]");
        dispatch(setCart(newCart));
      }
    };
    window.addEventListener("storage", handleStorage);
    return () => window.removeEventListener("storage", handleStorage);
  }, [dispatch]);

  return <Provider store={store}>{children}</Provider>;
}
