import {Label, TextInput} from "flowbite-react";
import {Field, ErrorMessage, FieldProps} from "formik";
import React from "react";

interface Props extends FieldProps {
	label: string;
	id: string;
	type?: string;
	placeholder?: string;
	name: string;
}

const CustomInput: React.FC<Props> = (props: Props) => {
	const error = props.form.errors[props.field.name];
	const borderClass = error ? "invalid-input" : "";

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
				name={props.field.name}
				component={"div"}
				className="text-red-800"
			/>
		</div>
	);
};

export default CustomInput;
