import axios from 'axios'

export default {
    data() {
      return { 
        requestCount: 0,
        requests: [],
        accountCreationRequests: [],
        roles: [],
        remedialActions: [],
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
        },
        currentRequestFiles: [],
        budget: {},
        profile: {},
        rateTable: [],
       }
    },
    methods: {
      async getRequestDetailsAPI(requestId, isRefresh) {
        await axios
        .get(this.API_URL + "v1/request/" + requestId)
        .then((response) => {
          if(isRefresh){
            this.currentRequestDetails.Notes = response.data.Notes;
          }
          else {
            this.currentRequestDetails = response.data;
          }
        })
        .catch((error) => {
            console.log(error)
        })
    
        await axios
        .get(this.API_URL + "v1/file/" + requestId)
        .then((response) => {
            this.currentRequestFiles = response.data;
            console.log(this.currentRequestFiles)
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async getRateTableInfo(county) {
        await axios
        .get(this.API_URL + "v1/request/ratetable/" + county)
        .then((response) => {
            this.rateTable = response.data;
            return this.rateTable;
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async GetRequestsAPI(id) {
        if(id){
          await axios
          .get(this.API_URL + "v1/request/provider/" + id)
          .then((response) => {
              this.requests = response.data;
              return this.requests;
          })
          .catch((error) => {
              console.log(error)
          })
        }
        else {
          await axios
          .get(this.API_URL + "v1/request/getallrequests")
          .then((response) => {
              this.requests = response.data;
              return this.requests;
          })
          .catch((error) => {
              console.log(error)
          })
        }
      },
      async GetRequestsStaffAPI(id) {
        console.log("it's this one")
        await axios
        .get(this.API_URL + "v1/request/employee/" + id)
        .then((response) => {
            this.requests = [];
            this.requests = this.requests.concat(response.data);
            return this.requests;
        })
        .catch((error) => {
            console.log(error)
        })

        console.log("it's this one")
        await axios
        .get(this.API_URL + "v1/request/draft")
        .then((response) => {
            this.requests = this.requests.concat(response.data);
            return this.requests;
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async GetRemedialActions() {
        await axios
        .get(this.API_URL + "v1/request/remedialactions/")
        .then((response) => {
            this.remedialActions = response.data;
            return this.remedialActions;
        })
        .catch((error) => {
            console.log(error)
        })
      },   
      async getAccountRequests() {
        await axios
        .get(this.API_URL + "v1/request/accountcreationrequests/")
        .then((response) => {
            this.accountCreationRequests = response.data;
            return this.remedialActions;
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async getRoles() {
        await axios
        .get(this.API_URL + "v1/role/roles/")
        .then((response) => {
            this.roles = response.data;
            return this.roles;
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async uploadFile(fileData, fileType, extension, requestId) {
        const url = `${this.API_URL}v1/file/filetype/${fileType}/requestId/${requestId}/ext/${extension}`;  
        try {
            const response = await axios.post(url, fileData, {
            headers: {
                'Content-Type': 'application/json'
            }
            });
            return response.data;
        } catch (error) {
            console.log(error);
        }
      },
      async getFiles(requestId) {
        await axios
        .get(this.API_URL + "v1/file/" + requestId)
        .then((response) => {
            this.currentRequestFiles = response.data;
            console.log(this.currentRequestFiles)
            return this.currentRequestFiles;
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async getBudget() {
        await axios
        .get(this.API_URL + "v1/budget")
        .then((response) => {
            this.budget = response.data;
            return this.budget;
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async getProfileAPI(wqcid) {
        await axios
        .get(this.API_URL + "v1/user/profile/" + wqcid)
        .then((response) => {
            this.profile = response.data;
            return this.profile;
        })
        .catch((error) => {
            console.log(error)
        })
      },
      async submitRequestAPI(request) {
        await axios
        .post(this.API_URL + "v1/request/add", request)
        .then((response) => {})
        .catch((error) => {
            console.log(error)
        })
      },
      async updateRequestAPI(request) {
        await axios
        .put(this.API_URL + "v1/request/update", request)
        .then((response) => {})
        .catch((error) => {
            console.log(error)
        })
      },
      async updateRequestStatusAPI(requestId, newStatus, isResent) {
        await axios
        .post(this.API_URL + "v1/status/" + requestId + "/" + newStatus)
        .then((response) => {})
        .catch((error) => {
            console.log(error)
        })
        var emailSendPath = newStatus.toLowerCase() == "new" && !isResent ? "newplan" :
                      newStatus.toLowerCase() == "new" && isResent ? "planresent" :
                      newStatus.toLowerCase() == "submitted" ? "planaccepted" :
                      newStatus.toLowerCase() == "approved" ? "planapproved" :
                      newStatus.toLowerCase() == "complete" && !isResent ? "plancompleted" :
                      newStatus.toLowerCase() == "complete" && isResent ? "planincomplete" :
                      newStatus.toLowerCase() == "paid" ? "planpaid" : "";
        if(emailSendPath != "") {
          await axios
          .post(this.API_URL + "v1/email/" + emailSendPath + "/" + requestId)
          .then((response) => {})
          .catch((error) => {
              console.log(error)
          })
        }
      },
      async updateApprovedDataAPI(request) {
        await axios
        .post(this.API_URL + "v1/requestdetail/approved", request)
        .then((response) => {})
        .catch((error) => {
            console.log(error)
        })
      },
    },
}