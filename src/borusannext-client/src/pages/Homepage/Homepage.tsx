import {useEffect} from "react";
import {BrandsApi} from "../../api";

type Props = {};

const Homepage = (props: Props) => {
	useEffect(() => {
		fetchBrands();
	}, []);

	const fetchBrands = async () => {
		const brandApi = new BrandsApi();

		const brands = await brandApi.apiBrandsGet(0, 50);

		console.log(brands);
	};

	return (
		<div className="flex items-center justify-center mt-5">
			<div className="grid grid-cols-12"></div>
		</div>
	);
};

export default Homepage;
