import type { App } from "vue";
import { clerkPlugin } from "@clerk/vue";
import { createPinia } from "pinia";
import router from "../router";
import vuetify from "./vuetify";

const PUBLISHABLE_KEY = import.meta.env.VITE_CLERK_PUBLISHABLE_KEY;

if (!PUBLISHABLE_KEY) {
  throw new Error("Add your Clerk Publishable Key to the .env file");
}

export function registerPlugins(app: App) {
  app.use(vuetify);
  app.use(createPinia());
  app.use(router);
  app.use(clerkPlugin, {
    publishableKey: PUBLISHABLE_KEY,
  });
}
