import axios from 'axios'

export default {
    data() {
      return { 
        requestCount: 0,
        jwt: ""
       }
    },
    methods: {
        submitLoginEmail(email) { 
            axios
                .get(this.API_URL)
                .then((response) => {
                    console.log(response)
                    requestCount = response.data.requestCount
                })
        },
        getToken() {
            axios
            .get(this.API_URL + "v1/user/login/")
            .then((response) => {
                console.log(response)
                jwt = response.data
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