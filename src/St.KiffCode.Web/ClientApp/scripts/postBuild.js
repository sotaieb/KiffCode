'use strict';

// import needed librairies
const path = require('path');
const fs = require('fs-extra');

// helper functions
const resolveApp = relativePath => path.resolve(appDirectory, relativePath);

process.on('unhandledRejection', err => {
    throw err;
});

//const argv = process.argv.slice(2);
//console.log(argv)
//console.log(argv.indexOf('--myarg') !== -1)

console.log("Checking module name...");

const appName = process.env.REACT_APP_NAME;
if (!appName) {
    throw new Error("Module not set !")
    // process.exit(1);
}

// configure paths
const appDirectory = fs.realpathSync(process.cwd());
const moduleDirectory = `${appDirectory}/../wwwroot/${appName}`
const layoutPath = `${appDirectory}/../Pages/Shared/_Layout_${appName}.cshtml`
const buildPath = resolveApp("./build");
const jsDirectoryPath = `${moduleDirectory}/static/js`;
const cssDirectoryPath = `${moduleDirectory}/static/css`;
const htmlOutputPath = `${moduleDirectory}/index.html`;

console.log("Clear module files...");
fs.emptyDirSync(moduleDirectory);

console.log("Copy files from build path...");
fs.copySync(buildPath, moduleDirectory);

console.log("Remove not needed js files...");
fs.readdirSync(jsDirectoryPath).filter(fn => fn.endsWith('.txt') ||
    fn.endsWith('.map')).forEach(element => {
        //if (fs.lstatSync(path.join(from, element)).isFile()) {        
        fs.removeSync(path.join(jsDirectoryPath, element));
        //} 
    });

console.log("Remove not needed css files...");
fs.readdirSync(cssDirectoryPath).filter(fn => fn.endsWith('.txt') ||
    fn.endsWith('.map')).forEach(element => {
        fs.removeSync(path.join(cssDirectoryPath, element));
    });

// update layout
fs.rename(htmlOutputPath, layoutPath);

// remove build path
fs.rmSync(buildPath, { recursive: true, force: true });

console.log("Operation succeeded !");
//process.exit(1);