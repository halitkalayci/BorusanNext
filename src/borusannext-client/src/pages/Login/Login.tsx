import {Button, Card, Label, TextInput} from "flowbite-react";
import {ErrorMessage, Field, Form, Formik} from "formik";
import React from "react";
import * as yup from "yup";
import CustomInput from "../../components/CustomInput/CustomInput";
import "./Login.css";
import {AuthApi} from "../../api";
type Props = {};

type LoginFormValues = {
	email: string;
	password: string;
};

const Login = (props: Props) => {
	const validationSchema = yup.object({
		email: yup.string().required().email(),
		password: yup.string().required().min(3).max(10), //.matches(),
	});

	const initialValues: LoginFormValues = {
		email: "",
		password: "",
	};

	const submit = async (formValues: LoginFormValues) => {
		const authApi = new AuthApi();

		const loginResponse = await authApi.apiAuthLoginPost({
			...formValues,
			authenticatorCode: "",
		});

		localStorage.setItem("token", loginResponse.data.accessToken?.token!);
	};

	return (
		<div className="flex min-w-full h-[100vh] justify-center items-center">
			<Card className="p-10 card">
				<h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
					Giriş Yap
				</h5>
				<Formik
					initialValues={initialValues}
					validationSchema={validationSchema}
					onSubmit={e => submit(e)}
				>
					<Form className="flex flex-col w-full">
						<Field
							label="E-posta"
							id="email"
							placeholder="user@borusan.com"
							name="email"
							type="email"
							component={CustomInput}
						/>
						<Field
							label="Şifre"
							id="password"
							name="password"
							type="password"
							component={CustomInput}
						/>
						<Button type="submit" className="mt-3">
							Submit
						</Button>
					</Form>
				</Formik>
			</Card>
		</div>
	);
};

export default Login;
