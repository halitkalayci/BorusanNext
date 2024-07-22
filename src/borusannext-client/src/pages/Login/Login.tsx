import {Button, Card, Label, TextInput} from "flowbite-react";
import {ErrorMessage, Field, Form, Formik} from "formik";
import React from "react";
import * as yup from "yup";

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

	return (
		<div className="flex w-full h-[100vh] justify-center items-center">
			<Card className="p-10">
				<h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
					Giriş Yap
				</h5>
				<Formik
					initialValues={initialValues}
					validationSchema={validationSchema}
					onSubmit={e => console.log(e)}
				>
					<Form className="flex flex-col w-full">
						<div>
							<div className="mb-2 block">
								<Label htmlFor="email1" value="E-posta" />
							</div>
							<Field
								id="email1"
								type="email"
								placeholder="name@flowbite.com"
								required
								name="email"
							/>
							<ErrorMessage
								name="email"
								component={"div"}
								className="text-red-500"
							/>
						</div>
						<div>
							<div className="mb-2 block">
								<Label htmlFor="password1" value="Şifre" />
							</div>
							<Field id="password1" type="password" required name="password" />
							<ErrorMessage
								name="password"
								component={"div"}
								className="text-red-500"
							/>
						</div>
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
