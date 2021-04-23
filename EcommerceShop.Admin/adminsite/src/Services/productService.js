import axios from "axios";
import { host ,Products} from "../config";
class productService {
  getList() {
    return axios({
      url: host + "/api/" + Products+"/"+"getAllProduct", 
      method: "get",
      actionName: `Get list products`,
    });
  }
  edit(id, object) {
    return axios({
      url: host + "/api/" + Products + "/" +"ProductId?ProductId="+ id,
      method: "put",
      data: object,
      actionName: `Edit product with ID: ${id}`,
    });
  }

  delete(id) {
    return axios({
      url: host + "/api/" + Products + "/" +"ProductId?ProductId="+ id,
      method: "delete",
      actionName: `Delete product with ID: ${id}`,
    });
  }
  create(object) {
    return axios({
      url: host + "/api/" + Products,
      method: "post",
      data: object,
      actionName: `Create new product`,
    });
  }

}

export default new productService();