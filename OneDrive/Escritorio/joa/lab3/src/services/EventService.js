import axios from "axios";

const apiClient = axios.create({
  baseURL: "https://my-json-server.typicode.com/JoaRampa/fakeapi",
  withCredentials: false,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});
const apiUrl = axios.create({
  baseURL: "https://criptoya.com/api/btc/ars/0.5",
  withCredentials: false,
});

export default {
  getEvents() {
    return apiClient.get("/events");
  },
  getEvent(id) {
    return apiClient.get("/events/" + id);
  },
  getApi(coin, fiat) {
    return apiUrl.get("/" + coin + "/" + fiat + "/1");
  },
  getApis() {
    return apiUrl.get();
  },
};
