import axios from 'axios'
import { useStore } from "vuex";

export default {
    data() {
      return { 
        requestCount: 0,
        isCurrentUser: false,
        emailProvided: "",
        tos: "",
        store: useStore(),
        htmlBody: {
            body: ""
        },
       }
    },
    methods: {
        async submitLoginEmail(email) { 
            await axios
            .post(this.API_URL + "v1/user/sendemail/" + email)
            .then((response) => {
                this.isCurrentUser = response.data;
                this.emailProvided = email;
                return this.currentUser;
            })
            .catch((error) => {
                console.log(error)
            })
        },
        async submitToken(token) {
            await axios
            .post(this.API_URL + "v1/user/login/" + this.emailProvided + "/" + token)
            .then((response) => {
                this.store.commit('updateJWT', response.data.Value)
            })
        },
        async getTOS() {
            await axios
            .get(this.API_URL + "v1/termsofservice")
            .then((response) => {
                this.tos = response.data;
            })
        },
        async updateTOS(newTOS) {
            this.htmlBody.body = newTOS
            await axios.put(this.API_URL + "v1/termsofservice", this.htmlBody)
            .then((response) => {
                console.log(response)
            })
        },
        async requestUserAccount(firstName, lastName, email) {
            await axios
            .post(this.API_URL + "v1/user/requestaccount/" + firstName + "/" + lastName + "/" + email)
            .then((response) => {
                console.log(this.response)
            })
        },
        getWeatherForecast() {
            console.log(this.API_URL + "WeatherForecast");
            axios
                .get(this.API_URL + "WeatherForecast/")
                .then((response) => {
                    console.log(response)
                })
        },
    },
}