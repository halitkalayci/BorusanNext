import {Button, Card, Label, Modal, TextInput} from "flowbite-react";
import {ErrorMessage, Field, Form, Formik} from "formik";
import React, {useContext, useState} from "react";
import * as yup from "yup";
import CustomInput from "../../components/CustomInput/CustomInput";
import "./Login.css";
import {AuthApi} from "../../api";
import {useNavigate} from "react-router-dom";
import {AUTHENTICATOR_TYPES} from "../../constants/authenticatorTypes";
import {AuthContext} from "../../contexts/AuthContext";
import {jwtDecode} from "jwt-decode";
import {CLAIM_NAMES} from "../../constants/jwtClaimNames";
type Props = {};

type LoginFormValues = {
	email: string;
	password: string;
	authenticatorCode?: string | null;
};

const Login = (props: Props) => {
	const navigate = useNavigate(); // hooks

	const [openModal, setOpenModal] = useState(false);
	const [modalText, setModalText] = useState("");

	const validationSchema = yup.object({
		email: yup.string().required().email(),
		password: yup.string().required().min(3).max(10), //.matches(),
	});

	const initialValues: LoginFormValues = {
		email: "",
		password: "",
		authenticatorCode: null,
	};
	const authContext = useContext(AuthContext);

	const submit = async (formValues: LoginFormValues) => {
		const authApi = new AuthApi();

		const loginResponse = await authApi.apiAuthLoginPost({
			...formValues,
		});

		if (loginResponse.data.accessToken) {
			const token = loginResponse.data.accessToken?.token!;
			localStorage.setItem("token", token);
			const decodedToken = jwtDecode<any>(token);
			authContext?.login({
				email: decodedToken[CLAIM_NAMES.EMAIL],
				id: decodedToken[CLAIM_NAMES.ID],
			});
			navigate("/");
		} else {
			switch (loginResponse.data.requiredAuthenticatorType) {
				case AUTHENTICATOR_TYPES.OTP:
					setModalText(
						"Lütfen otp uygulamanızdaki tek kullanımlık şifreyi giriniz.",
					);
					setOpenModal(true);
					break;
				case AUTHENTICATOR_TYPES.EMAIL:
					setModalText(
						"Lütfen e-posta adresinize gönderilen tek kullanımlık şifreyi giriniz.",
					);
					setOpenModal(true);
					break;
				default:
					break;
			}
		}
	};
	//TODO: Register sayfası tasarlanması.
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
					{e => {
						return (
							<Form className="flex flex-col w-full">
								<Field
									label="E-posta"
									id="email"
									placeholder="user@borusan.com"
									name="email"
									type="text"
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
								<Modal
									show={openModal}
									size="md"
									popup
									onClose={() => setOpenModal(false)}
								>
									<Modal.Header />
									<Modal.Body>
										<div className="space-y-6">
											<h3 className="text-xl font-medium text-gray-900 dark:text-white">
												{modalText}
											</h3>

											<Field
												label="Tek Kullanımlık Şifre"
												id="authenticatorCode"
												name="authenticatorCode"
												type="text"
												component={CustomInput}
											/>

											<div className="w-full">
												<Button type="submit" onClick={e.submitForm}>
													Submit
												</Button>
											</div>
										</div>
									</Modal.Body>
								</Modal>
							</Form>
						);
					}}
				</Formik>
			</Card>
		</div>
	);
};

export default Login;
