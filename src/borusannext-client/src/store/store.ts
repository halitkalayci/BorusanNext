import {configureStore} from "@reduxjs/toolkit";
import authReducer, {AuthState} from "./auth/authSlice";

export interface RootState {
	auth: AuthState;
}

export const store = configureStore({
	reducer: {
		auth: authReducer,
	},
});
// Boilerplate
export type AppDispatch = typeof store.dispatch;
