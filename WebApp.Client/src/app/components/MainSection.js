import visibilityFilters from '../constants/VisibilityFilters';

export default {
  templateUrl: 'src/app/components/MainSection.html',
  controller: ['lotteryService', MainSection],
  bindings: {
    draws: '=',
    filter: '<'
  }
};

/** @ngInject */
function MainSection(lotteriesService) {
  this.lotteriesService = lotteriesService;
  this.selectedFilter = visibilityFilters[this.filter];

  this.closedReducer = function (count, draw) {
    return draw.draw_status === "closed" ? count + 1 : count;
  };

  this.load();
}

MainSection.prototype = {

  handleShow: function (filter) {
    this.filter = filter;
    this.selectedFilter = visibilityFilters[filter];
  },

  load: function() {
    this.lotteriesService.getDraws().then((function(_this) {
        return function(response) {
            if (response && response.data.draws)
                _this.draws = response.data.draws;
        };
    })(this));
  }
};
