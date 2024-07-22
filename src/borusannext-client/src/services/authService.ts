import axios, {AxiosResponse} from "axios";
import {LoginRequestModel} from "../models/loginRequestModel";
import {LoginResponseModel} from "../models/loginResponseModel";

class AuthService {
	login(
		model: LoginRequestModel,
	): Promise<AxiosResponse<LoginResponseModel, any>> {
		return axios.post<LoginResponseModel>(
			"http://localhost:60805/api/Auth/Login",
			model,
		);
	}
}

export default new AuthService();
