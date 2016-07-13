export default {
  templateUrl: 'src/app/components/DrawItem.html',
  controller: DrawItem,
  bindings: {
    draw: '<',
    onChange: '&'
  }
};

function DrawItem() {
  this.editing = false;
}

DrawItem.prototype = {
  /*handleDoubleClick: function () {
    this.editing = true;
  },

  handleSave: function (text) {
    this.onSave({
      todo: {
        text: text,
        id: this.todo.id
      }
    });
    this.editing = false;
  },

  handleDestroy: function (id) {
    this.onDestroy({id: id});
  }*/
};
