import axios from 'axios'

export default {
    data() {
      return { 
        requestCount: 0
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