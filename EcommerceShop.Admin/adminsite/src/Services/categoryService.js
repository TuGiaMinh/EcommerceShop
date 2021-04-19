import axios from "axios";
import { host ,Categories} from "../config";
class cateService {
  getList() {
    return axios({
      url: host + "/" + Categories, 
      method: "get",
      actionName: `Get list categories`,
    });
  }
  edit(id, object) {
    return axios({
      url: host + "/" + Categories + "/" + id,
      method: "put",
      data: object,
      actionName: `Edit category with ID: ${id}`,
    });
  }

  delete(id) {
    return axios({
      url: host + "/" + Categories + "/" + id,
      method: "delete",
      actionName: `Delete category with ID: ${id}`,
    });
  }
  create(object) {
    return axios({
      url: host + "/" + Categories,
      method: "post",
      data: object,
      actionName: `Create new categories`,
    });
  }

}

export default new cateService();