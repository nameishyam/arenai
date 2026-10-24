import type { User } from "@/interfaces/entities";
import { defineStore } from "pinia";

export const useUserStore = defineStore("user", {
  state: () => ({
    user: null as User | null,
    isAuthenticated: false,
  }),

  getters: {
    isLoggedIn: (state) => state.isAuthenticated,
    currentUser: (state) => state.user,
  },

  actions: {
    login(user: User) {
      this.user = user;
      this.isAuthenticated = true;
    },

    logout() {
      this.user = null;
      this.isAuthenticated = false;
    },
  },
});
