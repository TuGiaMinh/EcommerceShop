import axios from 'axios';
import {host} from '../config';
const api = axios.create({
    baseURL: host,
});
export default api;