import axios from 'axios'

export default {
    data() {
      return { 
        requestCount: 0,
        currentRequestDetails: {
          createdAt: "",
          details: [],
          facility: "",
          id: 0,
          provider: "",
          status: null,
          totalCost: 0,
          totalCostLabor: 0,
          totalCostMaterials: 0
        }
       }
    },
    methods: {
      async getRequestDetailsAPI(requestId) {
          await axios
          .get(this.API_URL + "v1/request/" + requestId)
          .then((response) => {
              console.log(response)
              this.currentRequestDetails = response.data;
              return this.currentRequestDetails;
          })
          .catch((error) => {
              console.log(error)
          })
      },
      async getProviderRequestsAPI(id) {

      },
      getRequestCount() {
        console.log("in other mixin")
        axios
            .get(this.API_URL)
            .then((response) => {
                console.log(response)
                requestCount = response.data.requestCount
            })
      },
    },
}