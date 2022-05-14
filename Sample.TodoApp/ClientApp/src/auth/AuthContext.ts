import {createContext, Signal, useContext} from "solid-js";
import {LoginUser, LoginUserCommand} from "./loginPage/LoginUser";
import {LogoutUser} from "./LogoutUser";
import {SigninUser, SigninUserCommand} from "./SigninUser";

type AuthContextDefinition = {
    isAuthenticated: () => boolean;
    login: LoginUser;
    logout: LogoutUser;
    signin: SigninUser;
};
export const AuthContext = createContext<AuthContextDefinition>();

export const useAuth = () => useContext(AuthContext)!;

export const createAuthContext = (authSignal: Signal<boolean>): AuthContextDefinition => {
    const [isAuthenticated, _] = authSignal;
    return {
        isAuthenticated,
        login: (command: LoginUserCommand) => {
            throw new Error("not implemetned")
        },
        logout: () => {
            throw new Error("not implemetned")
        },
        signin: (command: SigninUserCommand) => {
            throw new Error("not implemetned");
        }
    };
};