import angular from 'angular';
import 'angular-mocks';
import DrawItem from './DrawItem.js';

describe('DrawItem component', function () {
  beforeEach(function () {
    angular
      .module('drawItem', ['src/app/components/DrawItem.html'])
      .component('drawItem', TodoItem);
    angular.mock.module('drawItem');
  });

  it('should render correctly', angular.mock.inject(function ($rootScope, $compile) {
    var $scope = $rootScope.$new();
    var element = $compile('<draw-item></draw-item>')($scope);
    $scope.$digest();
    var li = element.find('li');
    expect(li).not.toBeNull();
  }));

  /*it('should call set editing to true', angular.mock.inject(function ($componentController) {
    var component = $componentController('todoItem', {}, {});
    spyOn(component, 'handleDoubleClick').and.callThrough();
    component.handleDoubleClick();
    expect(component.handleDoubleClick).toHaveBeenCalled();
    expect(component.editing).toEqual(true);
  }));

  it('should call onSave', angular.mock.inject(function ($componentController) {
    var bindings = {
      todo: {
        text: 'Use ngrx/store',
        completed: false,
        id: 0
      },
      onSave: function () {}
    };
    var component = $componentController('todoItem', {}, bindings);
    spyOn(component, 'onSave').and.callThrough();
    component.handleSave('Hello');
    expect(component.onSave).toHaveBeenCalledWith({
      todo: {text: 'Hello', id: 0}
    });
  }));*/
});
