<template>
  <form @submit.prevent="register">
    <input v-model="username" placeholder="Username" required />
    <input v-model="password" type="password" placeholder="Password" required />
    <button type="submit">Register</button>
    <p v-if="errorMessage" style="color: red">{{ errorMessage }}</p>
  </form>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { api } from '../api';

const username = ref('');
const password = ref('');
const errorMessage = ref('');
const router = useRouter();

async function register() {
  errorMessage.value = '';

  try {
    const response = await api('/auth/register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        username: username.value,
        password: password.value,
      }),
    });

    const data = await response.json();

    if (data.isSuccess === true) {
      router.push('/login');
    } else {
      errorMessage.value = data.errors?.[0]?.message || 'Registration failed.';
    }
  } catch {
    errorMessage.value = 'An unexpected error occurred.';
  }
}
</script>
