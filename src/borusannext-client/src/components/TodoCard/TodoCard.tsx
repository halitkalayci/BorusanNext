import React from "react";
import {ToDoModel} from "../../models/toDoModel";

type Props = {
	todo: ToDoModel; // Zorunlu
	// todo? : ToDoModel // opsiyonel
};

const TodoCard = (props: Props) => {
	return (
		<div className="col-span-3 mb-3 mr-3">
			<div className="max-w-sm rounded overflow-hidden shadow-lg">
				<div className="px-6 py-4">
					<div className="font-bold text-xl mb-2">{props.todo.title}</div>
					<p className="text-red-700 text-base">{props.todo.id}</p>
				</div>
				<div className="px-6 pt-4 pb-2">
					<span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
						{props.todo.completed ? (
							<span className="text-green-600">Tamamlandı</span>
						) : (
							<span className="text-red-500">Tamamlanmadı</span>
						)}
					</span>
					<span>
						<button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
							Detaya Git
						</button>
					</span>
				</div>
			</div>
		</div>
	);
};

export default TodoCard;
