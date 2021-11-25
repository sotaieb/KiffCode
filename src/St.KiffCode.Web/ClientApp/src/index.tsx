import "./theme/main.scss";
import 'react-app-polyfill/ie11';
import 'react-app-polyfill/stable';
import React from 'react';
import { render } from "react-dom";
import reportWebVitals from './reportWebVitals';

const appName = process.env.REACT_APP_NAME;

if (process.env.REACT_APP_MODE === "STANDALONE") {
    var noscriptNode = document.createElement('noscript');
    noscriptNode.innerHTML = "You need to enable JavaScript to run this app.";
    var rootNode = document.createElement('div');
    rootNode.id = 'root';

    document.title = "KiffCode-Development";
    document.body.innerHTML = "";
    document.body.appendChild(noscriptNode);
    document.body.appendChild(rootNode);
}

(async () => {
    const app = await import(`./apps/${appName}`);
    const Component = app.default;

    render(
        <React.StrictMode>
            <Component />
        </React.StrictMode>,
        document.getElementById('root')
    );
})();


//import('./screens/home')
//    .then((module) => {
//        const Component = module.default;
//        render();
//    });

reportWebVitals();