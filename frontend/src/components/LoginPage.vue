<template>
    <form @submit.prevent="login">
      <input v-model="username" placeholder="Username" required />
      <input v-model="password" type="password" placeholder="Password" required />
      <button type="submit">Login</button>
    </form>
  </template>
  
  <script lang="ts" setup>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  import { api } from '../api';
  
  const username = ref('');
  const password = ref('');
  const router = useRouter();
  
  async function login() {
    const response = await api('/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username: username.value, password: password.value }),
    });
    const data = await response.json();
    localStorage.setItem('jwt', data.value.accessToken);
    router.push('/dashboard');
  }
  </script>