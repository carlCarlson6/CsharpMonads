import type { Component } from 'solid-js';
import { Routes, Route } from "solid-app-router"
import Main from "./Main";
import Login from "./auth/loginPage/Login";
import styles from "./assets/App.module.css";
import {createSignal} from "solid-js";
import {AuthContext, createAuthContext} from "./Auth/AuthContext";


const App: Component = () => {
    const authContext = createAuthContext(createSignal(false));
    
    return (
        <div class={styles.App}>
            <AuthContext.Provider value={authContext} >
                <Routes>
                    <Route path="/main" element={<Main/>}/>
                    <Route path="/" element={<Main/>}/>
                    <Route path="/login" element={<Login/>}/>
                </Routes>    
            </AuthContext.Provider>
        </div>
    );
}

export default App;
