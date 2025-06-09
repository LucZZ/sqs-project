import { createRouter, createWebHistory } from 'vue-router';
import RegisterPage from './components/RegisterPage.vue';
import LoginPage from './components/LoginPage.vue';
import DashboardPage from './components/Dashboard.vue';

const routes = [
    { path: '/', redirect: '/login' },
    { path: '/register', component: RegisterPage },
    { path: '/login', component: LoginPage },
    { path: '/dashboard', component: DashboardPage },
];

export default createRouter({
  history: createWebHistory(),
  routes,
});