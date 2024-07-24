import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import {AuthProvider} from "./contexts/AuthContext.tsx";
import {Provider} from "react-redux";
import {store} from "./store/store.ts";

ReactDOM.createRoot(document.getElementById("root")!).render(
	<AuthProvider>
		<Provider store={store}>
			<App />
		</Provider>
	</AuthProvider>,
);
