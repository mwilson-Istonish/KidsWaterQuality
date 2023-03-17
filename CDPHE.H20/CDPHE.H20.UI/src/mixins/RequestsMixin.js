import axios from 'axios'

export default {
    data() {
      return { 
        requestCount: 0
       }
    },
    methods: {
      getRequestCount() { 
        axios
            .get(this.API_URL)
            .then((response) => {
                console.log(response)
                requestCount = response.data.requestCount
            })
        }
    },
}