import {useEffect, useState} from "react";
import {BrandsApi, GetListBrandListItemDtoGetListResponse} from "../../api";
import {Pagination, Table} from "flowbite-react";

type Props = {};

const Homepage = (props: Props) => {
	const [brandList, setBrandList] =
		useState<GetListBrandListItemDtoGetListResponse>();

	const [currentPage, setCurrentPage] = useState(1);

	useEffect(() => {
		fetchBrands();
	}, [currentPage]);

	const fetchBrands = async () => {
		const brandApi = new BrandsApi();

		const brands = await brandApi.apiBrandsGet(currentPage - 1, 1);

		setBrandList(brands.data);
	};

	return (
		<div className="flex items-center justify-center mt-5">
			<div className="overflow-x-auto">
				{brandList && (
					<>
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
											{item.logo && (
												<img
													src={item.logo!}
													width={250}
													height={250}
													alt="logo"
													// onError={}
												/>
											)}
										</Table.Cell>
									</Table.Row>
								))}
							</Table.Body>
						</Table>
						<Pagination
							currentPage={brandList?.index! + 1}
							totalPages={brandList?.pages!}
							onPageChange={page => setCurrentPage(page)}
						/>
					</>
				)}
			</div>
		</div>
	);
};

export default Homepage;
