import {Button} from "flowbite-react";

type Props = {};

const Homepage = (props: Props) => {
	return (
		<div className="flex items-center justify-center mt-5">
			<div className="grid grid-cols-12">
				<Button>Tıkla</Button>
			</div>
		</div>
	);
};

export default Homepage;
