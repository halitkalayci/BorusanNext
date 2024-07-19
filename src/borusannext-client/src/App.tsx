import {useState} from "react";
import "./App.css"; // css import
import Header from "./Header";

// RFC => React Functional Component
// JS + HTML => JSX

function App() {
	// Fonksiyonalite
	let x: number = 0;
	// State => içerisindeki değişkenler değiştiğinde UI update edilir.

	// count {get; set;}
	// State içerisinde count isminde bir değişken oluşturur.
	const [count, setCount] = useState<number>(0);

	function onPlusClick() {
		setCount(count + 1);
		x++;
		console.log(x);
	}
	function onMinusClick() {
		setCount(count - 1);
		x--;
		console.log(x);
	}
	// var,let => ARAŞTIRMA
	// const => sabitler

	// { JS kodu }
	return (
		<>
			<Header></Header>
			<button onClick={onPlusClick}>+</button>
			<button onClick={onMinusClick}>-</button>
			<p>{x}</p>
			<p>{count}</p>
		</>
	);
}

// export
export default App; // ilgili dosyanın main içeriğini ifade ediyor.
// c# => public protected
