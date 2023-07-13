<script setup>
import { RouterLink, RouterView } from 'vue-router'
import AdminSettings from '@/components/modals/AdminSettings.vue'
</script>

<template>
    <div class="h20-dashboard-view h20-full-height">
        <div class="row h20-full-height">
            <div class="col">
                <div class="card h20-full-height">
                    <div class="card-body" style="padding:2rem;">
                        <div class="row">
                            <div class="col-xl-2 h20-dashboard-col">
                                <div class="card h20-dashboard-card">
                                    <span v-show="getNotificationCount > 0" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" style="font-size:16px">
                                        {{getNotificationCount}}
                                    </span>
                                    <div class="card-body text-center h20-dashboard-card-body" v-on:click="navigateToPage('/Requests')">
                                        My <br>Requests<br>
                                        <i class="fa-solid fa-envelope h20-dashboard-card-icon"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-2 h20-dashboard-col" v-show="this.store.getters.waterForKidsAccess">
                                <div class="card h20-dashboard-card">
                                    <div class="card-body text-center h20-dashboard-card-body" v-on:click="navigateToPage('/WaterForKids')">
                                        Water for Kids Reimbursement<br>
                                        <i class="fa-solid fa-faucet-drip h20-dashboard-card-icon"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-2 h20-dashboard-col" v-show="this.store.getters.userManagementAccess">
                                <div class="card h20-dashboard-card">
                                    <div class="card-body text-center h20-dashboard-card-body" v-on:click="navigateToPage('/UserManagement')">
                                        User <br>Management<br>
                                        <i class="fa-solid fa-users h20-dashboard-card-icon"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-2 h20-dashboard-col">
                                <div class="card h20-dashboard-card">
                                    <div class="card-body text-center h20-dashboard-card-body" data-bs-toggle="modal" data-bs-target="#MyProfileModal">
                                        My <br>Profile<br>
                                        <i class="fa-solid fa-user-gear h20-dashboard-card-icon"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-2 h20-dashboard-col" v-show="this.store.getters.isAdmin">
                                <div class="card h20-dashboard-card">
                                    <div class="card-body text-center h20-dashboard-card-body" data-bs-toggle="modal" data-bs-target="#AdminSettingsModal">
                                        Admin <br>Settings<br>
                                        <i class="fa-solid fa-lock h20-dashboard-card-icon"></i>
                                    </div>
                                </div>
                            </div>
                        </div>      
                        <!-- <span v-on:click="incrementCount()">{{ $store.state.count }}</span> -->
                    </div>
                </div>
            </div>
        </div>
    </div>
<AdminSettings v-show="store.getters.isAdmin"/>
</template>

<script>
  import RequestsMixin from '../mixins/RequestsMixin'
  import { useStore } from "vuex";
  export default {
    components: {
        AdminSettings
    },
    data () {
      return {
        requestCount: 1,
        store: useStore(),
      }
    },     
    mixins: [RequestsMixin],
    async created() {
        if(this.store.getters.getUserRole == "Provider"){
            await this.GetRequestsAPI(this.store.getters.getUser.Id);
        }
        else if(this.store.getters.getUserRole == "User Manager") {
            await this.getAccountRequests();
        }
        else {
            console.log("Staff")
            await this.GetRequestsStaffAPI(this.store.getters.getUser.Id);
        }   
    },

    methods: {
        navigateToPage(page) {
            this.$router.push(page)
        },
        incrementCount() {
            this.store.commit('increment', 2)
        }
    },
    computed: {
        getNotificationCount() {
            console.log("notifications")
            if (this.requests.length > 0) {
                if (this.store.getters.getUserRole == "Provider") {
                    return this.requests.filter(g => g.Status.toLowerCase() == "new" || g.Status.toLowerCase() == "approved").length
                }
                else if (this.store.getters.getUserRole == "WQ Staff") {
                    return this.requests.filter(g => g.Status.toLowerCase() == "draft" || g.Status.toLowerCase() == "submitted" || g.Status.toLowerCase() == "complete").length
                }
                else if (this.store.getters.getUserRole == "User Manager") {
                    return this.accountCreationRequests.length
                }
                else {
                    return 0
                }
            }
            else {
                return 0;
            }

        }
    }
  }
</script>