<template>
    <div class="h20-router-view">
        <div class="row">
            <div class="col-lg-4 col-md-3"></div>
            <div class="col-lg-4 col-md-6">
                <div class="card">
                    <div class="card-body" style="padding:2rem">
                        <div class="text-center" style="font-weight:600; font-size:22px">
                            Welcome to the Water Quality Portal
                        </div>
                        <div style="margin-top:2rem; font-size: 18px">
                            <label>Enter your email to log in or create an account:</label>
                            <div class="input-group mb-3">
                                <input v-model="email" type="text" class="form-control" placeholder="example@email.com">
                            </div>
                        </div>
                        <div>
                            <button class="btn btn-md h20-btn" id="EmailSubmitBtn" v-on:click="submitEmail()">Submit</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-3"></div>
        </div>
    </div>
</template>

<script>
import LoginMixin from '../mixins/LoginMixin'
export default {
    mixins: [ LoginMixin ],
    data() {
        return {
            email: ""
        }
    },
    created() {
        this.getWeatherForecast();
    }, 
    methods: {
        submitEmail() {
            var buttonEle = document.getElementById("EmailSubmitBtn");
            buttonEle.disabled = true;
            this.$router.push("/Dashboard")
        }
    }
}
</script>