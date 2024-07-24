import {configureStore} from "@reduxjs/toolkit";
import authReducer, {AuthState} from "./auth/authSlice";

// ** genel depo tanımı
export interface RootState {
	auth: AuthState;
}

// ** alt depoları genel depoya eklemek.
export const store = configureStore({
	reducer: {
		auth: authReducer,
	},
});
// Boilerplate
export type AppDispatch = typeof store.dispatch;
