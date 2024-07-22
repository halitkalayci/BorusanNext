import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./pages/Login/Login";
import MainLayout from "./layouts/MainLayout/MainLayout";
import {mainLayoutRoutes, noLayoutRoutes} from "./routes/routes";

const App = () => {
	return (
		<>
			<BrowserRouter>
				<Routes>
					{mainLayoutRoutes.map(route => (
						<Route
							path={route.path}
							element={<MainLayout>{route.element}</MainLayout>}
						/>
					))}
					{noLayoutRoutes.map(route => (
						<Route path={route.path} element={route.element} />
					))}
				</Routes>
			</BrowserRouter>
		</>
	);
};

export default App;
