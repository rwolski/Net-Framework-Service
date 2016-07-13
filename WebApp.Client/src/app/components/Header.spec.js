import angular from 'angular';
import 'angular-mocks';
import Header from './Header.js';

describe('Header component', function () {
  var draws = [
    {
      draw_id: 1
      draw_name: 'Test Draw',
      draw_date: '2016-05-16',
      draw_status: 'open',
      draw_numbers: []
    }
  ];

  function MockLotteryService() {
  }

  MockLotteryService.prototype.addDraw = function (draws, draw) {
    return draws.push(draw);
  };

  beforeEach(function () {
    angular
      .module('headerComponent', ['src/app/components/Header.html'])
      .service('lotteryService', MockLotteryService)
      .component('headerComponent', Header);
    angular.mock.module('headerComponent');
  });

  it('should render correctly', angular.mock.inject(function ($rootScope, $compile) {
    var element = $compile('<header-component></header-component>')($rootScope);
    $rootScope.$digest();
    var header = element.find('h1');
    expect(header.html().trim()).toEqual('Lottery Draws');
  }));

  /*it('should get the draws binded to the component', angular.mock.inject(function ($rootScope, $compile, $componentController) {
    var component = $componentController('headerComponent', {}, {draws: draws});
    spyOn(component, 'handleSave').and.callThrough();
    expect(component.draws.length).toEqual(1);
    component.handleSave('New Task');
    expect(component.handleSave).toHaveBeenCalledWith('New Task');
    expect(component.draws.length).toEqual(2);
  }));*/
});
