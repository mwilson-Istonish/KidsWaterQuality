<template>
    <div class="h20-dashboard-view h20-full-height">
    <div class="row h20-full-height">
        <div class="col">
            <div class="card h20-full-height">
                <div class="card-body" style="padding:2rem;">
                    <div class="row">
                        <div class="col-xl-2">
                            <div class="card h20-dashboard-card">
                                <div class="card-body text-center h20-dashboard-card-body">
                                    My <br>Requests<br>
                                    <i class="fa-solid fa-envelope h20-dashboard-card-icon"></i>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-2">
                            <div class="card h20-dashboard-card">
                                <div class="card-body text-center h20-dashboard-card-body">
                                    Water for Kids Reimbursement<br>
                                    <i class="fa-solid fa-faucet-drip h20-dashboard-card-icon"></i>
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
        email: undefined,
        phoneNumber: undefined,
        message: undefined
      }
    },
      
    methods: {
        submitContactForm() {
            var buttonEle = document.getElementById("ContactFormSubmitBtn");
            buttonEle.disabled = true;
        }
    }
  }
</script>