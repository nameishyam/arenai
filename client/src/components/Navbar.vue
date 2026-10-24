<script setup lang="ts">
import { useTheme } from "vuetify";
import { watch } from "vue";
import { RouterLink } from "vue-router";
import { useUserStore } from "@/stores/user";

const theme = useTheme();
const userStore = useUserStore();

watch(
  () => theme.global.name.value,
  (newTheme: string) => {
    localStorage.setItem("theme", newTheme);
  },
  { immediate: true },
);

function toggleTheme(e: MouseEvent) {
  theme.setTransitionOrigin(e.target as HTMLElement);
  theme.toggle();
}
</script>

<template>
  <v-app-bar app>
    <RouterLink to="/" class="ml-8 no-underline">
      <div
        class="flex cursor-pointer items-center gap-1 text-xl font-bold transition-opacity hover:opacity-80"
      >
        <v-icon
          icon="mdi-movie-open-play-outline"
          color="primary"
          class="opacity-90"
        />
      </div>
    </RouterLink>
    <v-spacer />
    <div class="flex items-center gap-10">
      <div v-if="!userStore.isAuthenticated">
        <v-btn> Signup </v-btn>
        <v-btn> Login </v-btn>
      </div>
      <div v-else>loggedin</div>
      <v-btn icon="mdi-theme-light-dark" @click="toggleTheme" />
    </div>
  </v-app-bar>
</template>
