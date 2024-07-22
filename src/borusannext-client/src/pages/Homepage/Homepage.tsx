import React, {useEffect, useState} from "react";
import toDoService from "../../services/toDoService";
import {ToDoModel} from "../../models/toDoModel";
import TodoCard from "../../components/TodoCard/TodoCard";

type Props = {};

const Homepage = (props: Props) => {
	const [todoList, setTodoList] = useState<ToDoModel[]>([]);

	useEffect(() => {
		fetchTodos();
	}, []);

	const fetchTodos = async () => {
		const response = await toDoService.fetchAll();
		setTodoList(response.data);
	};

	// Component

	// React Router'da parametre okuma url'edn
	return (
		<div className="flex items-center justify-center mt-5">
			<div className="grid grid-cols-12">
				{todoList.map(todo => {
					return <TodoCard todo={todo} />;
				})}
			</div>
		</div>
	);
};

export default Homepage;
