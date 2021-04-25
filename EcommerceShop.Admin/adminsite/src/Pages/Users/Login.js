import React, { Fragment, useState } from 'react'
import api from'../../Utils/api';
import { history } from '../../Helpers/History';

export const Login = () => {

    const token = localStorage.getItem("token");
    const info = JSON.parse(localStorage.getItem("info"));

    if(token!==null&&info.roles==='admin'){
        history.push("/home");
    }

    const [input, setInput] = useState({
        username: "",
        password: ""
    });

    const handleChange = (e) => {
        const value = e.target.value;
        setInput({
            ...input,
            [e.target.name]: value
        });
    }

    const {username,password}=input;

    const handleSubmit = async (e)=>{
        e.preventDefault();
        if(username && password){
            var urlencoded = new URLSearchParams();
            urlencoded.append("grant_type", "password");
            urlencoded.append("username", username);
            urlencoded.append("password", password);
            urlencoded.append("client_id", "react");
            urlencoded.append("client_secret", "secret");
            const result = await api.post('/connect/token',urlencoded).then(r => { return r.data });
            let info = await api.get("/api/v1/Users", {headers: { Authorization: `Bearer `+result.access_token }}).then(r=>{return r.data});
            if(info.roles==="admin"){
                localStorage.setItem("token",result.access_token);
                localStorage.setItem("info",JSON.stringify(info));
                history.push("/home");
            }
        }
    };

    return (
        <Fragment>
            <form onSubmit={handleSubmit}>
                <div>
                    <div className="col-sm-6">
                        <div className="form-group">
                            <label className="control-label col-sm-4">Partner Email ID</label>
                            <div className="col-sm-8">
                                <input type="text" className="form-control" name="username" onChange={handleChange} />
                            </div>
                        </div>
                        <div className="form-group">
                            <label className="control-label col-sm-4">Password</label>
                            <div className="col-sm-8">
                                <input type="password" className="form-control" name="password" onChange={handleChange} />
                            </div>
                        </div>
                    </div>
                    <div className="text-center">
                        <button className="btn btn-primary waves-effect waves-light " type="submit" id="btn-submit">Login</button>
                    </div>
                </div>
            </form>
        </Fragment >



    )
}
