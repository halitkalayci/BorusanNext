// Slice => Parça

import {createSlice} from "@reduxjs/toolkit";
import {jwtDecode} from "jwt-decode";
import {CLAIM_NAMES} from "../../constants/jwtClaimNames";

// ** State'in arayüzü. (State hangi türden hangi isimde bilgilere sahip olacak.)
export interface AuthState {
	isAuthenticated: boolean;
	user: any; // modellemek best practice
}
const getInitialAuthState = (): AuthState => {
	const token = localStorage.getItem("token");

	if (token) {
		const decodedToken = jwtDecode<any>(token);
		return {
			isAuthenticated: true,
			user: {
				email: decodedToken[CLAIM_NAMES.EMAIL],
				id: decodedToken[CLAIM_NAMES.ID],
			},
		};
	} else return {isAuthenticated: false, user: null};
};

// ** Uygulama açılışında state hangi durumda başlıyacak
const initialState: AuthState = getInitialAuthState();

export const authSlice = createSlice({
	name: "auth",
	initialState,
	reducers: {
		login: (state, action) => {
			state.isAuthenticated = true;
			state.user = action.payload;
		},
		logout: state => {
			state.isAuthenticated = false;
			state.user = null;
		},
	}, // State'i değiştirecek aksiyonlar
});

export const {login, logout} = authSlice.actions;

export default authSlice.reducer;
