const path = require('path');
const autoprefixer = require('autoprefixer');
const cssnano = require('cssnano');


// const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const TerserPlugin = require('terser-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin

const mode = process.env.NODE_ENV || "development";

const isProduction = mode === "production";
const isDevelopment = mode === "development";
module.exports = {
    mode,
    devtool: isDevelopment ? "source-map" : false,
    entry:
    {
        main: path.resolve(__dirname, 'Scripts/screens/home/index.js'),
    },

    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname, './wwwroot/dist'),
        assetModuleFilename: 'assets/[hash][ext][query]',
        clean: true,
    },
    optimization: {
        minimize: isProduction,       
        minimizer: [new TerserPlugin({
            terserOptions: {
                parse: {
                    ecma: 8,
                },
                compress: {
                    ecma: 5,
                    warnings: false,                    
                    comparisons: false,                    
                    inline: 2,
                },
                mangle: {
                    safari10: true,
                },
                keep_classnames: isProduction,
                keep_fnames: isProduction,
                output: {
                    ecma: 5,
                    comments: false,                   
                    ascii_only: true,
                },
            }
        })],
    },
    target: ['web', 'es5'],
    module: {
        rules: [

            {
                test: /\.(js|jsx|ts|tsx)$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        babelrc: false,
                        presets: ["@babel/preset-env", "@babel/preset-typescript",
                            ["@babel/preset-react", { runtime: "automatic" }]
                        ],
                        plugins: [
                            [
                                "@babel/plugin-transform-runtime",
                                {
                                    absoluteRuntime: false,
                                    corejs: 3,
                                    version: "^7.16.3",
                                    targets: "ie >= 10",
                                },
                            ],
                        ],
                    }
                }
            },
            {
                test: /\.(s[ac]|c)ss$/i,
                exclude: /(node_modules)/,
                use: [                   
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            //publicPath: ""
                        }
                    },
                    {
                        loader: "css-loader",
                        options:
                        {
                            //url: false,
                            //importLoaders: 1
                        }
                    },
                    {
                        loader: 'postcss-loader', options: {
                            postcssOptions: {
                                plugins: [autoprefixer(), cssnano()]
                            }
                        }
                    },
                    { loader: 'sass-loader' },
                ],
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: "asset/resource"
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/i,
                //type: 'asset/resource', //asset/inline
                type: "asset",
                parser: {
                    dataUrlCondition: {
                        maxSize: 10 * 1024
                    }
                }
            },

        ],
    },
    resolve: {
        extensions: [".ts", ".tsx", ".js", ".jsx"],
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: '[name].bundle.css',
            chunkFilename: '[id].css'
        }),
        new BundleAnalyzerPlugin(),
    ],

};