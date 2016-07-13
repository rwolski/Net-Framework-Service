import angular from 'angular';
import 'angular-mocks';
import MainSection from './MainSection.js';

describe('MainSection component', function () {
  function MockLotteriesService() {}
  MockLotteriesService.prototype = {
  };

  var component;

  beforeEach(function () {
    angular
      .module('mainSection', ['src/app/components/MainSection.html'])
      .service('lotteriesService', MockLotteriesService)
      .component('mainSection', MainSection);
    angular.mock.module('mainSection');
  });

  beforeEach(angular.mock.inject(function ($componentController) {
    component = $componentController('mainSection', {}, {});
  }));

  it('shoud set selectedFilter', function () {
    component.handleShow('show_completed');
    expect(component.selectedFilter.type).toEqual('show_completed');
    expect(component.selectedFilter.filter({completed: true})).toEqual(true);
  });
});
