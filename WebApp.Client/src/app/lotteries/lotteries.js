import 'es6-shim';

var initialDraws = [
{
  draw_id: 1,
  draw_name: 'Powerball 1',
  draw_numbers: [1, 2, 3],
  draw_date: '2016-05-16',
  draw_status: 'closed'
},
{
  draw_id: 2,
  draw_name: 'Powerball 2',
  draw_numbers: [4, 5, 6],
  draw_date: '2016-05-18',
  draw_status: 'open'
}];

function LotteryService() {
}

LotteryService.prototype = {
  addDraw: function (draws, draw) {
    return draws.push(draw);
  },
   
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

