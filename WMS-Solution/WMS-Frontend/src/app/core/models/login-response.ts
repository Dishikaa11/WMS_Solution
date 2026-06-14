export interface LoginResponse {

  token:string;

  username:string;

  role:string;

  lastLogin:string;

  isPasswordChanged:boolean;
}
