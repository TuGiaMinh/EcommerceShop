import axios from 'axios';
import { host, Brands } from '../config';
class BrandService {
    getToken(){
        const token = localStorage.getItem("token");
    }
    getList() {
        return axios({
            url: host + "/api/" + Brands, 
            method: "get",
            actionName: `Get list brands`,
          });
    }
    edit(id, object) {
        return axios({
            url: host + "/api/" + Brands + "/" + id,
            method: "put",
            data: object,
            actionName: `Edit brand with ID: ${id}`,
        });
    }
    delete(id) {
        
        return axios({
            url: host + "/api/" + Brands + "/" + id,
            method: "delete",
            actionName: `Delete brand with ID: ${id}`,
        });
    }
    create(object) {
        return axios({
            url: host + "/api/" + "Brands",
            method: "post",
            data: object,
            actionName: `Create new brand`,
        });
    }
}
export default new BrandService();