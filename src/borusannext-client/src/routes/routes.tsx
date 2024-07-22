import {lazy} from "react";
import React from "react";

interface RouteConfig {
	path: string;
	element: React.ReactNode;
}

const Homepage = lazy(() => import("../pages/Homepage/Homepage"));
const Login = lazy(() => import("../pages/Login/Login"));

export const mainLayoutRoutes: RouteConfig[] = [
	{path: "", element: <Homepage />},
];

export const noLayoutRoutes: RouteConfig[] = [
	{path: "login", element: <Login />},
];
