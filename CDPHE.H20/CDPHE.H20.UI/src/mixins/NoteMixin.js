import axios from 'axios'

export default {
    data() {},
    methods: {
        async addNoteAPI(newNote) {
            await axios
            .post(this.API_URL + "v1/note/add", newNote)
            .then((response) => {})
            .catch((error) => {
                console.log(error)
            })
        }
    },
}