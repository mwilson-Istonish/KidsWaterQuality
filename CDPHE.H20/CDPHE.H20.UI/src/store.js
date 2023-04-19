import Vuex from 'vuex'

const store = new Vuex.Store({
  state: {
    title: "Vue Store",
    isLoggedIn: false,
    userRole: "WQ Infrastructure Staff",
    waterForKidsRoles: ["WQ Infrastructure Staff", "WQ Fiscal Staff", "Facility User"],
    userManagementRoles: ["User Approver"],
    count: 1
  },
  mutations: {
    increment (state, incrementNum) {
      state.count = state.count + incrementNum;
    },
    changeLoggedInStatus (state, status) {
        state.isLoggedIn = status;
    },
    updateCount (state, newCount) {
        state.count = newCount;
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
    isLoggedIn(state) {
        return state.isLoggedIn;
    },
    getCount(state) {
        return state.count;
    }
  },
  actions:{}
});

export default store;