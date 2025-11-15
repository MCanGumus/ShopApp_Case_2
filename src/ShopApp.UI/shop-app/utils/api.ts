// utils/api.ts
export const API_URL = "http://localhost:7072/api";

interface FetchOptions extends RequestInit {
  auth?: boolean;
}

export async function apiFetch(endpoint: string, options: FetchOptions = {}) {
  const { auth = true, ...rest } = options;

  let accessToken = auth ? sessionStorage.getItem("accessToken") : null;

  let response = await fetch(`${API_URL}${endpoint}`, {
    ...rest,
    headers: {
      "Content-Type": "application/json",
      ...(auth && accessToken ? { Authorization: `Bearer ${accessToken}` } : {}),
      ...rest.headers,
    },
  });

  if (response.status === 401 && auth) {
    const refreshed = await refreshAccessToken();
    if (!refreshed) {
      sessionStorage.clear();
      window.location.href = "/login";
      return null;
    }

    accessToken = sessionStorage.getItem("accessToken");

    response = await fetch(`${API_URL}${endpoint}`, {
      ...rest,
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${accessToken}`,
        ...rest.headers,
      },
    });
  }

  return response;
}

async function refreshAccessToken(): Promise<boolean> {
  const refreshToken = sessionStorage.getItem("refreshToken");
  if (!refreshToken) return false;

  try {
    const res = await fetch(`${API_URL}/User/RefreshToken`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ refreshToken }),
    });

    if (!res.ok) return false;

    const data = await res.json();
    sessionStorage.setItem("accessToken", data.access_token);
    sessionStorage.setItem("refreshToken", data.refresh_token);
    return true;
  } catch (err) {
    console.error("Refresh token hatası:", err);
    return false;
  }
}
