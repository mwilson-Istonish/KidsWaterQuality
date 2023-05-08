import axios from 'axios'

export default {
    data() {
      return { 
        requestCount: 0,
        requests: [],
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
              this.currentRequestDetails = response.data;
              return this.currentRequestDetails;
          })
          .catch((error) => {
              console.log(error)
          })
      },
      async GetRequestsByProviderID(id) {
          await axios
          .get(this.API_URL + "v1/request/provider/" + id)
          .then((response) => {
              this.requests = response.data;
              return this.requests;
          })
          .catch((error) => {
              console.log(error)
          })
      },
      getRequestCount() {
        axios
            .get(this.API_URL)
            .then((response) => {
                console.log(response)
                requestCount = response.data.requestCount
            })
      },
    },
}