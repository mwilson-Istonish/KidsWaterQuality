import axios from 'axios'
import { useStore } from "vuex";

export default {
    data() {
      return { 
        Users: []
       }
    },
    methods: {
        async getUsers() {
            await axios
            .get(this.API_URL + "v1/user/all")
            .then((response) => {
                console.log(response.data)
            })
        },
    },
}