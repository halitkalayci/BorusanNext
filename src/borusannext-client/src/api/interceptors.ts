import {AxiosInstance} from "axios";

export const setupInterceptors = (axiosInstance: AxiosInstance) => {
	axiosInstance.interceptors.request.use(config => {
		console.log("interceptor devrede..");

		const token = localStorage.getItem("token");
		if (token) config.headers.Authorization = "Bearer " + token;
		return config;
	});

	axiosInstance.interceptors.request.use(
		response => {
			return response;
		},
		error => {
			// Error handling..
			// TODO: Global ex. handling.
			return Promise.reject(error);
		},
	);
};
