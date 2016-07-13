import angular from 'angular';
import 'todomvc-app-css/index.css!';

//import todos from './app/todos/todos.js';
import lotteries from './app/lotteries/lotteries.js';
import App from './app/containers/App.js';
import Header from './app/components/Header.js';
import MainSection from './app/components/MainSection.js';
import DrawItem from './app/components/DrawItem.js';
import Footer from './app/components/Footer.js';
import 'angular-ui-router';
import routesConfig from './routes.js';

angular
  .module('app', ['ui.router'])
  .config(routesConfig)
  .service('lotteryService', lotteries.LotteryService)
  .component('app', App)
  .component('headerComponent', Header)
  .component('footerComponent', Footer)
  .component('mainSection', MainSection)
  .component('drawItem', DrawItem);
