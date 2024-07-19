import {BrowserRouter, Route, Routes} from "react-router-dom";
import Header from "./components/Header/Header";
import Homepage from "./pages/Homepage/Homepage";
import Login from "./pages/Login/Login";

const App = () => {
	return (
		<>
			<BrowserRouter>
				<Header />
				<Routes>
					<Route path={""} element={<Homepage />} />
					<Route path={"/login"} element={<Login />} />
				</Routes>
			</BrowserRouter>
		</>
	);
};

export default App;
