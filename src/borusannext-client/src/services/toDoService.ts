import axios, {AxiosResponse} from "axios";
import {ToDoModel} from "../models/toDoModel";

class ToDoService {
	fetchAll(): Promise<AxiosResponse<ToDoModel[], any>> {
		return axios.get<ToDoModel[]>("https://jsonplaceholder.typicode.com/todos");
	}
}

export default new ToDoService();
