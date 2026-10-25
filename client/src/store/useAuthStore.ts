import { type AuthState, type User } from "@/lib/interfaces"
import { create } from "zustand"

export const useAuthStore = create<AuthState>((set) => ({
  user: null as User | null,
  isAuthenticated: false as boolean | false,

  setUser: (user: User) =>
    set({
      user,
      isAuthenticated: true,
    }),

  logout: () =>
    set({
      user: null,
      isAuthenticated: false,
    }),
}))
