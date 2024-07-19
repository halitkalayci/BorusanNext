import {useEffect, useState} from "react";
import "./App.css"; // css import
import Header from "./Header";

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

	return (
		<>
			<Header></Header>
			<button
				onClick={() => {
					onPlusClick();
				}}
			>
				+
			</button>
			<button onClick={onMinusClick}>-</button>
			<p>{count}</p>

			<hr />
			{/* Two Way Data Binding */}
			<input
				onChange={e => {
					setTodo(e.target.value);
				}}
				value={todo}
				type="text"
				placeholder="Add to do"
			/>
			<button>Add</button>
			<ul>
				{/* Iterasyon  => Key */}
				{toDoList.map(todo => (
					<li key={todo.id}>{todo.title}</li>
				))}
			</ul>
		</>
	);
}

// export
export default App; // ilgili dosyanın main içeriğini ifade ediyor.
// c# => public protected
