import { createRouter, createWebHistory } from 'vue-router'
import store from '@/store.js';
import axios from 'axios'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      redirect: '/Login'
    },
    {
      path: '/Login',
      name: 'Login',
      meta: {
        allowAnonymous: true,
        authRequired: false,
      },
      component: () => import('../views/Login.vue')
    },
    {
      path: '/ContactUs',
      name: 'ContactUs',
      meta: {
        allowAnonymous: true,
        authRequired: false,
      },
      component: () => import('../views/ContactUs.vue')
    },
    {
      path: '/Dashboard',
      name: 'Dashboard',
      meta: {
        allowAnonymous: false,
        authRequired: false,
      },
      component: () => import('../views/Dashboard.vue')
    },
    {
      path: '/Requests',
      name: 'Requests',
      meta: {
        allowAnonymous: false,
        authRequired: false,
      },
      component: () => import('../views/Requests.vue')
    },
    {
      path: '/AccountCreationRequests',
      name: 'AccountCreationRequests',
      meta: {
        allowAnonymous: false,
        authRequired: false,
      },
      component: () => import('../views/AccountCreationRequests.vue')
    },
    {
      path: '/UserManagement',
      name: 'UserManagement',
      meta: {
        allowAnonymous: false,
        authRequired: true,
      },
      component: () => import('../views/UserManagement.vue')
    },
    {
      path: '/WaterForKids',
      name: 'WaterForKids',
      meta: {
        allowAnonymous: false,
        authRequired: true,
      },
      component: () => import('../views/WaterForKids.vue')
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'Any',
      redirect: '/Login'
    }
  ]
});

function unauthorized() {
  router.push('/Dashboard')
}

function unauthenticated() {
  store.commit('removeJWT')
  router.push('/Login')
}

axios.interceptors.request.use(
  async (config) => {
    const jwt = store.getters.getJWT;
    if (jwt != null) {
      config.headers.authorization = `Bearer ${jwt}`
    }
    return config;
  },
)

axios.interceptors.response.use(
  function(response) {
      return response
  },
  function(error) {
      console.log(error)
      if (error.response.status === 401) {
          store.commit('removeJWT')
          router.push('/Login')
      }
  }
);

router.beforeEach((to, from, next) => {
  console.log(to, from)
  var jwt = JSON.parse(localStorage.getItem('jwt'))
  if(to.meta.authRequired) {
    if (to.name == "UserManagement" && !store.getters.userManagementAccess) {
      unauthorized()
    }
    if (to.name == "WaterForKids" && !store.getters.waterForKidsAccess) {
      unauthorized()
    }
  }
  if(!to.meta.allowAnonymous && jwt == null) {
    console.log("router unauthenticated")
    unauthenticated()
  }
  if(to.name == "Requests" && store.getters.getUserRole == "User Manager"){
    router.push('/AccountCreationRequests')
  }

  return next()
});

export default router