
import Vuex from 'vuex'

const store = new Vuex.Store({
  state: {
    title: "Vue Store",
    userRole: "Admin",
    waterForKidsRoles: ["WQ Staff", "WQ Fiscal", "Provider"],
    userManagementRoles: ["User Manager"],
    count: 1,
    jwtToken: JSON.parse(localStorage.getItem("jwt")),
  },
  mutations: {
    increment (state, incrementNum) {
      state.count = state.count + incrementNum;
    },
    updateCount (state, newCount) {
        state.count = newCount;
    },
    updateJWT (state, jwtToken) {
      //have to also set state to make it reactive in other components, but it pulls from localstorage in the get method
      state.jwtToken = jwtToken;
      localStorage.setItem('jwt', JSON.stringify(jwtToken));
    },
    removeJWT (state) {
      //have to also set state to make it reactive in other components, but it pulls from localstorage in the get method
      state.jwtToken = null   
      localStorage.removeItem('jwt');
      state.user = null   
      localStorage.removeItem('user');
    }
  },
  getters: {
    isAdmin(state) {
        return state.userRole == "Admin"
    },
    waterForKidsAccess(state) {
        return state.waterForKidsRoles.includes(state.userRole);
    },
    userManagementAccess(state, getters) {
        return state.userManagementRoles.includes(state.userRole) || getters.isAdmin
    },
    getCount(state) {
        return state.count;
    },
    getJWT(state) {
      return state.jwtToken
    },
    getUser(state) {
      if(state.jwtToken != null) {
        var base64Url = state.jwtToken.split('.')[1];
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        var jsonUser = decodeURIComponent(window.atob(base64).split('').map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
        
        return JSON.parse(jsonUser)
      }
      return null
    },
    getUserRole(state, getters) {
      return getters.getUser ? getters.getUser['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] : null;
    },
    isLoggedIn(state) {
      return state.jwtToken != null;
    }
  },
  actions:{}
});

export default store;