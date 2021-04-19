import axios from 'axios';
import { host, Brands } from '../config';
class brandService {
    getList() {
        return axios({
            url: host + "/" + Brands, 
            method: "get",
            actionName: `Get list brands`,
          });
    }
    edit(id, object) {
        return axios({
            url: host + "/" + Brands + "/" + id,
            method: "put",
            data: object,
            actionName: `Edit brand with ID: ${id}`,
        });
    }
    delete(id) {
        return axios({
            url: host + "/" + Brands + "/" + id,
            method: "delete",
            actionName: `Delete brand with ID: ${id}`,
        });
    }
    create(object) {
        return axios({
            url: host + "/" + "Brands",
            method: "post",
            data: object,
            actionName: `Create new brand`,
        });
    }
}
export default new brandService();