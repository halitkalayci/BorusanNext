import {useEffect, useState} from "react";
import {BrandsApi, GetListBrandListItemDtoGetListResponse} from "../../api";
import {Button, Pagination, Table, TextInput} from "flowbite-react";
import * as signalR from "@microsoft/signalr";

type Props = {};

const Homepage = (props: Props) => {
	const [message, setMessage] = useState("");
	const [messageList, setMessageList] = useState<string[]>([]);
	const [connection, setConnection] = useState<signalR.HubConnection | null>(
		null,
	);

	useEffect(() => {
		const token = localStorage.getItem("token") || "";
		const newConnection = new signalR.HubConnectionBuilder()
			.withUrl("http://localhost:60805/chathub?access_token=" + token)
			.withAutomaticReconnect()
			.build();

		setConnection(newConnection);
	}, []);

	useEffect(() => {
		startConnection();
	}, [connection]);

	const startConnection = async () => {
		await connection?.start();
		connection?.on("NewMessage", message => {
			console.log("Canlı yeni bir veri geldi:" + message);
			setMessageList([...messageList, message]);
		});
		console.log(`Hub bağlantısı başarılı id:${connection?.connectionId}`);
	};

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

	const sendMessage = () => {
		connection?.invoke("SendMessage", message);
	};

	// Brand listesi
	// TODO: Araba listesi sayfalama ile birlikte tablo kullanmadan (tercihen Card) listelenmeli.
	// TODO: Sayfalama pagination ile değil infinite scroll ile yapılmalı. (BUTONla çözebiliriz.)
	return (
		<div className="flex items-center justify-center mt-5">
			<div className="overflow-x-auto">
				<TextInput
					type="text"
					value={message}
					onChange={e => setMessage(e.target.value)}
				/>
				<Button
					onClick={() => {
						sendMessage();
					}}
				>
					Gönder
				</Button>
				<ul>
					{messageList.map(m => (
						<li>{m}</li>
					))}
				</ul>
			</div>
		</div>
	);
};

export default Homepage;
// 11:30
