import {BrowserRouter, Route, Routes} from "react-router-dom";
import Homepage from "./pages/Homepage/Homepage";
import Login from "./pages/Login/Login";
import MainLayout from "./layouts/MainLayout/MainLayout";

const App = () => {
	return (
		<>
			<BrowserRouter>
				<Routes>
					<Route
						path={""}
						element={
							<MainLayout>
								<Homepage />
							</MainLayout>
						}
					/>
					<Route path={"/login"} element={<Login />} />
				</Routes>
			</BrowserRouter>
		</>
	);
};

export default App;
