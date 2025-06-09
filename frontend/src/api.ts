const BASE_URL = import.meta.env.VITE_API_BASE_URL;

export async function api(path: string, options: RequestInit = {}) {
  return fetch(`${BASE_URL}${path}`, options);
}