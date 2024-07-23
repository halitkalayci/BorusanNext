import {AxiosInstance} from "axios";
import {EXCEPTION_TYPES} from "../constants/exceptionTypes";

export const setupInterceptors = (axiosInstance: AxiosInstance) => {
	axiosInstance.interceptors.request.use(config => {
		console.log("interceptor devrede..");

		const token = localStorage.getItem("token");
		if (token) config.headers.Authorization = "Bearer " + token;
		return config;
	});

	axiosInstance.interceptors.response.use(
		response => {
			return response;
		},
		error => {
			// Error handling..
			console.log("HATA INTERCEPTORDE YAKALNDI:", error);
			const errorData = error.response.data;

			//TODO: Bütün hata türlerini yönet. Kullanıcıyı bilgilendir (toastr vs opsiyonel.)
			switch (errorData.type) {
				case EXCEPTION_TYPES.VALIDATION:
					break;
				case EXCEPTION_TYPES.BUSINESS:
					alert(errorData.detail);
					break;
				case EXCEPTION_TYPES.AUTHORIZATION:
					break;
				default:
					break;
			}

			return Promise.reject(error);
		},
	);
};
