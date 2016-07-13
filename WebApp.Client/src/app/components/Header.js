export default {
  templateUrl: 'src/app/components/Header.html',
  controller: ['lotteryService', Header],
  bindings: {
    draws: '='
  }
};

/** @ngInject */
function Header(lotteryService) {
  this.lotteryService = lotteryService;
}

Header.prototype = {
  /*handleSave: function (text) {
    if (text.length !== 0) {
      this.todos = this.todoService.addTodo(text, this.todos);
    }
  }*/
};
