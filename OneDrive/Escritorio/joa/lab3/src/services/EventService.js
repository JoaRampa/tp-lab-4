import axios from 'axios'

const apiClient = axios.create({
	baseURL: 'https://my-json-server.typicode.com/JoaRampa/fakeapi',
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
    }
})

const apiUrl = 'https://www.cryptoya.com/api/btc/ars/1';

export default {
    getEvents(){
        return apiClient.get('/events')
    },
    getEvent(id){
        return apiClient.get('/events/' + id)
    }
}
