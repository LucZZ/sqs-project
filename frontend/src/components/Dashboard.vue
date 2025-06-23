<template>
  <div>
    <form @submit.prevent="addUrl">
      <input v-model="newUrl" placeholder="Original URL" required />
      <button type="submit">Shorten</button>
    </form>
    <p v-if="errorMessage" style="color: red">{{ errorMessage }}</p>

    <table>
      <thead>
        <tr><th>Original URL</th><th>Short URL</th></tr>
      </thead>
      <tbody>
        <tr v-for="entry in urls" :key="entry.id">
          <td>{{ entry.originalUrl }}</td>
          <td>{{ entry.shortUrl }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { api } from '../api';

interface UrlEntry {
  id: string;
  originalUrl: string;
  shortUrl: string;
}

const urls = ref<UrlEntry[]>([]);
const newUrl = ref('');
const errorMessage = ref('');

async function fetchUrls() {
  try {
    const res = await api('/urls', {
      headers: { Authorization: `Bearer ${localStorage.getItem('jwt')}` },
    });
    const data = await res.json();
    urls.value = data.value;
  } catch (e) {
    errorMessage.value = 'Failed to load URLs.';
  }
}

async function addUrl() {
  errorMessage.value = '';

  try {
    const res = await api('/urls', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('jwt')}`,
      },
      body: JSON.stringify({ url: newUrl.value }),
    });

    const data = await res.json();

    if (data.isSuccess === true) {
      urls.value.push(data.value);
      newUrl.value = '';
    } else {
      errorMessage.value = data.errors?.[0]?.message || 'URL shortening failed.';
    }
  } catch {
    errorMessage.value = 'An unexpected error occurred.';
  }
}

onMounted(fetchUrls);
</script>
