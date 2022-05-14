import {User} from "./LoginUser";

export type SigninUserCommand = { userName: string, password: string };

export type SigninUser = (command: SigninUserCommand) => Promise<User>;