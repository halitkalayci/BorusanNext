import {Button, Card, Label, TextInput} from "flowbite-react";
import React from "react";

type Props = {};

const Login = (props: Props) => {
	return (
		<div className="flex w-full h-[100vh] justify-center items-center">
			<Card className="p-10">
				<form className="flex flex-col w-full">
					<div>
						<div className="mb-2 block">
							<Label htmlFor="email1" value="E-posta" />
						</div>
						<TextInput
							id="email1"
							type="email"
							placeholder="name@flowbite.com"
							required
						/>
					</div>
					<div>
						<div className="mb-2 block">
							<Label htmlFor="password1" value="Şifre" />
						</div>
						<TextInput id="password1" type="password" required />
					</div>
					<Button type="submit" className="mt-3">
						Submit
					</Button>
				</form>
			</Card>
		</div>
	);
};

export default Login;
