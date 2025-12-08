const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = async (env, options) => {
  const dev = options.mode === 'development';
  const config = {
    devtool: dev ? 'eval-source-map' : 'source-map',
    entry: {
      polyfill: ['core-js/stable', 'regenerator-runtime/runtime'],
      taskpane: './src/taskpane/taskpane.js',
      commands: './src/commands/commands.js',
    },
    output: {
      clean: true,
    },
    resolve: {
      extensions: ['.html', '.js'],
    },
    module: {
      rules: [
        {
          test: /\.js$/,
          exclude: /node_modules/,
          use: {
            loader: 'babel-loader',
            options: {
              presets: ['@babel/preset-env'],
            },
          },
        },
        {
          test: /\.html$/,
          exclude: /node_modules/,
          use: 'html-loader',
        },
        {
          test: /\.(png|jpg|jpeg|gif|ico)$/,
          type: 'asset/resource',
          generator: {
            filename: 'assets/[name][ext][query]',
          },
        },
        {
          test: /\.css$/,
          use: ['style-loader', 'css-loader'],
        },
      ],
    },
    plugins: [
      new HtmlWebpackPlugin({
        filename: 'taskpane.html',
        template: './src/taskpane/taskpane.html',
        chunks: ['polyfill', 'taskpane'],
      }),
      new HtmlWebpackPlugin({
        filename: 'commands.html',
        template: './src/commands/commands.html',
        chunks: ['polyfill', 'commands'],
      }),
      new CopyWebpackPlugin({
        patterns: [
          {
            from: 'assets/*',
            to: 'assets/[name][ext][query]',
          },
        ],
      }),
    ],
    devServer: {
      port: 3000,
      headers: {
        'Access-Control-Allow-Origin': '*',
      },
      server: {
        type: 'https',
      },
      hot: true,
    },
  };
  return config;
};
