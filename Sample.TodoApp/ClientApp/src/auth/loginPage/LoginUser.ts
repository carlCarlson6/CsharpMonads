export type LoginUserCommand = { userName: string, password: string };

export type User = { id: string, userName: string, password: string };

export type LoginUser = (command: LoginUserCommand) => Promise<User>; 