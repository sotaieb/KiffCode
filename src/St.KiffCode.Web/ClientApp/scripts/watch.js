'use strict';

// configure env variables
process.env.NODE_ENV = "development";
process.env.PUBLIC_URL = `/${process.env.REACT_APP_NAME}`;
process.env.FAST_REFRESH = false;

// import needed librairies
const fs = require("fs-extra");
const webpack = require("webpack");
const paths = require("react-scripts/config/paths");
const config = require("react-scripts/config/webpack.config")(process.env.NODE_ENV);
const ReactRefreshWebpackPlugin = require('@pmmmwh/react-refresh-webpack-plugin');

// configure paths
const appName = process.env.REACT_APP_NAME
const outputPath = `${paths.appPath}/../wwwroot/${appName}`;
const layoutPath = `${paths.appPath}/../Pages/Shared/_Layout_${appName}.cshtml`;
const htmlPath = `${outputPath}/index.html`;

// update webpack config
config.output.path = outputPath;
config.entry = config.entry.filter((f) => !f.match(/webpackHotDevClient/));
config.plugins = config.plugins.filter((p) => !(p instanceof webpack.HotModuleReplacementPlugin)
    && !(p instanceof ReactRefreshWebpackPlugin));

// watch files
(async () => {
    await fs.emptyDir(outputPath)
    webpack(config).watch({}, (err) => {
        if (err) {
            console.error(err);
        } else {
            fs.copySync(paths.appPublic, outputPath, {
                dereference: true,
                filter: file => file !== paths.appHtml
            });
            if (fs.existsSync(htmlPath)) {
                fs.rename(htmlPath, layoutPath);
            }
            
            console.log("Compile succeeded !");
        }
    });
})();