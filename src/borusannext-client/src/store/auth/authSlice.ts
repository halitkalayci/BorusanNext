// Slice => Parça

import {createSlice} from "@reduxjs/toolkit";

export interface AuthState {
	isAuthenticated: boolean;
	user: any;
}

const initialState: AuthState = {isAuthenticated: false, user: null};

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
