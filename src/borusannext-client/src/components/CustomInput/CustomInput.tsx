import {Label, TextInput} from "flowbite-react";
import {Field, ErrorMessage} from "formik";
import React from "react";

type Props = {
	label: string;
	id: string;
	type?: string;
	placeholder?: string;
	name: string;
	field: any;
	form: any;
};

const CustomInput: React.FC<Props> = (props: Props) => {
	console.log(props);
	const error = props.form.errors[props.field.name];
	const borderClass = error ? "border-red-500" : "";

	return (
		<div>
			<div className="mb-2 block">
				<Label htmlFor={props.id} value={props.label} />
			</div>
			<TextInput
				id={props.id}
				type={props.type || "text"}
				placeholder={props.placeholder}
				className={borderClass}
				{...props.field}
			/>
			<ErrorMessage
				name={props.name}
				component={"div"}
				className="text-red-800"
			/>
		</div>
	);
};

export default CustomInput;
