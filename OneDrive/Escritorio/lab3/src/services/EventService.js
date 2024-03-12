import axios from 'axios'

const apiClient = axios.create({
	baseURL: 'https://laboratorio-afe2.restdb.io/rest/',
	headers: {'x-apikey': '650b53356888544ec60c00bf'}
});

export default {
    getEvents(){
        return apiClient.get('/events')
    }
}
