// CTRL + ALT + V

import {AccessToken} from "./accessToken";

export interface LoginResponseModel {
	accessToken: AccessToken;
	requiredAuthenticatorType?: number;
}
