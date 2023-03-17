<template>
    <div class="h20-dashboard-view h20-full-height">
    <div class="row h20-full-height">
        <div class="col">
            <div class="card h20-full-height">
                <div class="card-body" style="padding:2rem;">
                    <div class="row">
                        <div class="col-xl-2 h20-dashboard-col">
                            <div class="card h20-dashboard-card">
                                <span v-show="requestCount > 0" class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger" style="font-size:16px">
                                    {{requestCount}}
                                </span>
                                <div class="card-body text-center h20-dashboard-card-body" v-on:click="navigateToPage('/Requests')">
                                    My <br>Requests<br>
                                    <i class="fa-solid fa-envelope h20-dashboard-card-icon"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-2 h20-dashboard-col">
                            <div class="card h20-dashboard-card">
                                <div class="card-body text-center h20-dashboard-card-body">
                                    Water for Kids Reimbursement<br>
                                    <i class="fa-solid fa-faucet-drip h20-dashboard-card-icon"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-2 h20-dashboard-col">
                            <div class="card h20-dashboard-card">
                                <div class="card-body text-center h20-dashboard-card-body">
                                    User <br>Management<br>
                                    <i class="fa-solid fa-user-gear h20-dashboard-card-icon"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</template>

<script>
  export default {
    data () {
      return {
        requestCount: 1
      }
    },
      
    methods: {
        navigateToPage(page) {
            console.log(this.API_URL)
            this.$router.push(page)
        }
    }
  }
</script>