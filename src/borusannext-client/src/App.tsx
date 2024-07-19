import {useEffect, useState} from "react";
import "./App.css"; // css import
import Header from "./Header";
import {ToDoModel} from "./models/toDoModel";
import axios from "axios";

// RFC => React Functional Component
// JS + HTML => JSX

function App() {
	// Fonksiyonalite
	// let x: number = 0;
	// State => içerisindeki değişkenler değiştiğinde UI update edilir.

	// count {get; set;}
	// State içerisinde count isminde bir değişken oluşturur.

	// useX() => React Hooks
	const [count, setCount] = useState<number>(0);

	// Sayfa açılışını yakalamak.

	// useEffect
	// 10:20

	// ES6 -> Arrow Function Syntax
	// function onPlusClick() {
	// }
	useEffect(() => {
		console.log("Sayfa açıldı");
	}, []);

	// n adet useEffect olabilir.
	useEffect(() => {
		console.log("Count değişti:" + count);
	}, [count]);

	const onPlusClick = () => {
		setCount(count + 1);
	};

	const onMinusClick = () => {
		setCount(count - 1);
	};

	const [toDoList, setToDoList] = useState([
		{id: 1, title: "Todo 1"},
		{id: 2, title: "Todo 2"},
		{id: 3, title: "Todo 3"},
	]);

	// var,let => ARAŞTIRMA
	// const => sabitler

	// { JS kodu }
	const [todo, setTodo] = useState<string>("");

	const addToDo = () => {
		// Destructring Assignment
		setToDoList([...toDoList, {id: Math.random(), title: todo}]);
		setTodo("");
	};

	const [dynamicToDoList, setDynamicToDoList] = useState<ToDoModel[]>([]);

	useEffect(() => {
		fetchToDos();
	}, []);

	const fetchToDos = async () => {
		// https://jsonplaceholder.typicode.com/todos

		// 2 şekil
		// await - async
		// promise - then - catch
		// const response = await fetch("https://jsonplaceholder.typicode.com/todos");
		// const json = await response.json();
		// setDynamicToDoList(json);
		// console.log(json);

		// fetch("https://jsonplaceholder.typicode.com/todos")
		// 	.then(response => response.json())
		// 	.then(json => setDynamicToDoList(json))
		// 	.catch(e => console.log("İstek hatalı"));

		axios
			.get<ToDoModel[]>("https://jsonplaceholder.typicode.com/todos")
			.then(response => {
				setDynamicToDoList(response.data);
			});
	};

	return (
		<>
			<Header></Header>
			<p className="text-xl text-blue-700">Merhaba</p>
		</>
	);
}

// export
export default App; // ilgili dosyanın main içeriğini ifade ediyor.
// c# => public protected
