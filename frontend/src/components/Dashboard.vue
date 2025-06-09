<template>
    <div>
      <form @submit.prevent="addUrl">
        <input v-model="newUrl" placeholder="Original URL" required />
        <button type="submit">Shorten</button>
      </form>
  
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
  import { api } from '@/api';
  
  interface UrlEntry {
    id: string;
    originalUrl: string;
    shortUrl: string;
  }
  
  const urls = ref<UrlEntry[]>([]);
  const newUrl = ref('');
  
  async function fetchUrls() {
    const res = await api('/urls', {
      headers: { Authorization: `Bearer ${localStorage.getItem('jwt')}` },
    });
    urls.value = await res.json();
  }
  
  async function addUrl() {
    const res = await api('/urls', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('jwt')}`,
      },
      body: JSON.stringify({ originalUrl: newUrl.value }),
    });
    const added = await res.json();
    urls.value.push(added);
    newUrl.value = '';
  }
  
  onMounted(fetchUrls);
  </script>