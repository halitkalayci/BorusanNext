import {createContext, useState, ReactNode} from "react";

interface AuthContextType {
	authData: any;
	login: (data: any) => void;
}

export const AuthContext = createContext<AuthContextType | undefined>(
	undefined,
);

export const AuthProvider: React.FC<{children: ReactNode}> = ({children}) => {
	const [authData, setAuthData] = useState({
		isAuthenticated: false,
		userData: null,
	});

	const login = (data: any) => {
		setAuthData({isAuthenticated: true, userData: data});
	};

	return (
		<AuthContext.Provider value={{authData, login}}>
			{children}
		</AuthContext.Provider>
	);
};
