import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import WalletView from "@/views/WalletView.vue";
import HistoryView from "@/views/ViewHistory.vue";
import store from "@/store";
import InvestmentsCrypto from "@/components/InvestmentsCrypto.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: HomeView,
    meta: { requiereGuest: true },
  },
  {
    path: "/wallet",
    name: "WalletView",
    component: WalletView,
    meta: { requiereAuth: true },
  },
  {
    path: "/history",
    name: "HistoryView",
    component: HistoryView,
    meta: { requiereAuth: true },
  },
  {
    path: "/investments",
    name: "investmentsCrypto",
    component: InvestmentsCrypto,
    meta: { requiereAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.meta.requiresAuth && !store.state.isAuthenticated) {
    alert("Ingrese un userId");
    next("/");
  } else if (to.meta.requiresGuest && store.state.isAuthenticated) {
    next("/#");
  } else {
    next();
  }
});

export default router;
