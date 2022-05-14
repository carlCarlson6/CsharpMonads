import {Component} from "solid-js";
import {useAuth} from "../AuthContext";
import {LoginUserCommand} from "./LoginUser";
import {createStore} from "solid-js/store";

const userLoginForm = () => {
    const [form, setForm] = createStore<LoginUserCommand>({
       userName: "",
       password: "" 
    });
    
    return { 
        form,
        updateFormField: (fieldName: string) => (event: Event) => {
            const inputElement = event.currentTarget as HTMLInputElement;
            setForm({
                [fieldName]: inputElement.value
            });
        }, 
        clearField: (fieldName: string) => {
            setForm({
                [fieldName]: ""
            });
        }
    };
};

const Login: Component = () => {
    const auth = useAuth();
    const { form, updateFormField } = userLoginForm();
    const handleOnSubmit = async (event: Event) => {
        event.preventDefault();
        const user = await auth.login(form);
    }
    
    
    return (
        <>
            <form onSubmit={() => {throw new Error("not implemented")}}>
                <div class="form-control">
                    <label for="userName">UserName:</label>
                    <input
                        type="text"
                        id="userName"
                        value={form.userName}
                        onChange={updateFormField("userName")}
                    />
                </div>
                <div class="form-control">
                    <label for="password">Password:</label>
                    <input
                        type="text"
                        id="password"
                        value={form.password}
                        onChange={updateFormField("password")}
                    />
                </div>
                <input class="form-submit" type="submit" value="Submit order" />
            </form>
        </>       
    );
};

export default Login;