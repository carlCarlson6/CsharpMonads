import {Component, lazy} from "solid-js";

type Route = {
    path: string,
    component: Component & {preload: () => Promise<{default: Component}>}
    children: Route[]
}

export const routes: Route[] = [
    {
        path: "/main",
        component: lazy(() => import("./Main")),
        children: []
    },
    {
        path: "/",
        component: lazy(() => import("./Main")),
        children: []
    }
];