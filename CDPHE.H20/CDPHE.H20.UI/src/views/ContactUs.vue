<template>
    <div class="h20-router-view">
        <div class="row">
            <div class="col-lg-4 col-md-3"></div>
            <div class="col-lg-4 col-md-6">
                <div class="card">
                    <div class="card-body" style="padding:2rem">
                        <div class="text-center" style="font-weight:600; font-size:22px">
                            Contact Us<br>
                        </div>
                        <div class="text-center">
                            Provide your contact information and a message below
                        </div>
                        <div style="margin-top:2rem; font-size: 18px">
                            <label>Email:</label>
                            <div class="input-group mb-3">
                                <input v-model="email" type="text" class="form-control" placeholder="example@email.com">
                            </div>
                            <label>Phone Number:</label>
                            <div class="input-group mb-3">
                                <input v-model="phoneNumber" type="number" class="form-control" placeholder="(123) 456-7890">
                            </div>
                            <label>Message:</label>
                            <div class="input-group mb-3">
                                <textarea rows="3" v-model="message" type="number" class="form-control" placeholder="What can we help you with?" />
                            </div>
                        </div>
                        <div>
                            <button class="btn btn-md h20-btn" id="ContactFormSubmitBtn" v-on:click="submitContactForm()">Submit</button>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <RouterLink to="/Login">Back to Login</RouterLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 col-md-3"></div>
        </div>
    </div>
</template>

<script>
import router from '../router';

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