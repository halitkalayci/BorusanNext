import {useEffect, useState} from "react";
import {BrandsApi, GetListBrandListItemDtoGetListResponse} from "../../api";
import {Table} from "flowbite-react";

type Props = {};

const Homepage = (props: Props) => {
	const [brandList, setBrandList] =
		useState<GetListBrandListItemDtoGetListResponse>();

	useEffect(() => {
		fetchBrands();
	}, []);

	const fetchBrands = async () => {
		const brandApi = new BrandsApi();

		const brands = await brandApi.apiBrandsGet(0, 50);

		setBrandList(brands.data);
	};

	return (
		<div className="flex items-center justify-center mt-5">
			<div className="overflow-x-auto">
				<Table>
					<Table.Head>
						<Table.HeadCell>Id</Table.HeadCell>
						<Table.HeadCell>İsim</Table.HeadCell>
						<Table.HeadCell>Logo</Table.HeadCell>
					</Table.Head>
					<Table.Body className="divide-y">
						{brandList?.items?.map(item => (
							<Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800">
								<Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
									{item.id}
								</Table.Cell>
								<Table.Cell>{item.name}</Table.Cell>
								<Table.Cell>
									<img src={item.logo!} width={250} height={250} />
								</Table.Cell>
							</Table.Row>
						))}
					</Table.Body>
				</Table>
			</div>
		</div>
	);
};

export default Homepage;
