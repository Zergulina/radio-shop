import axios from "axios"

export const loginApi = (login : string, password: string): Promise<any> => {
    return axios.post("/api/users/login", {userName: login, password : password})
}