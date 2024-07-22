import React from "react";
import Header from "../../components/Header/Header";

type Props = React.PropsWithChildren<{}>;

const MainLayout = (props: Props) => {
	return (
		<>
			<Header />
			{props.children}
			<footer>Footer</footer>
		</>
	);
};

export default MainLayout;
