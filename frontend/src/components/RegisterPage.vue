<template>
    <form @submit.prevent="register">
      <input v-model="username" placeholder="Username" required />
      <input v-model="password" type="password" placeholder="Password" required />
      <button type="submit">Register</button>
    </form>
  </template>
  
  <script lang="ts" setup>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  import { api } from '@/api';
  
  const username = ref('');
  const password = ref('');
  const router = useRouter();
  
  async function register() {
    await api('/auth/register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username: username.value, password: password.value }),
    });
    router.push('/login');
  }
  </script>