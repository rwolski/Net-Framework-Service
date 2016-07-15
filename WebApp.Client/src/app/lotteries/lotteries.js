import 'es6-shim';

var initialDraws = [
{
  draw_number: 1,
  draw_winning_numbers: [1, 2, 3],
  draw_date_time: '2016-05-16',
  draw_status: 'closed'
},
{
  draw_number: 2,
  draw_winning_numbers: [4, 5, 6],
  draw_date_time: '2016-05-18',
  draw_status: 'open'
}];

function LotteryService(http, serverUrl) {
    this.$http = http;
    this.request = function (endpoint) {
        return serverUrl + endpoint;
    }
}

LotteryService.prototype = {
  addDraw: function (draws, draw) {
    return draws.push(draw);
  },

  getDraws: function () {
      return this.$http.get(this.request('lottery/draws'));
  }

  /*completeTodo: function (id, todos) {
    return todos.map(function (todo) {
      return todo.id === id ?
        Object.assign({}, todo, {completed: !todo.completed}) :
        todo;
    });
  },

  deleteTodo: function (id, todos) {
    return todos.filter(function (todo) {
      return todo.id !== id;
    });
  },

  editTodo: function (id, text, todos) {
    return todos.map(function (todo) {
      return todo.id === id ?
        Object.assign({}, todo, {text: text}) :
        todo;
    });
  },

  completeAll: function (todos) {
    var areAllMarked = todos.every(function (todo) {
      return todo.completed;
    });
    return todos.map(function (todo) {
      return Object.assign({}, todo, {completed: !areAllMarked});
    });
  },

  clearCompleted: function (todos) {
    return todos.filter(function (todo) {
      return todo.completed === false;
    });
  }*/
};

export default {
  LotteryService: LotteryService,
  initialDraws: initialDraws
};

