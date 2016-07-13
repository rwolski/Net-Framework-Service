import lotteryFilters from '../constants/LotteryFilters';

export default {
  templateUrl: 'src/app/components/Footer.html',
  controller: Footer,
  bindings: {
    closedCount: '<',
    openCount: '<',
    selectedFilter: '<filter',
    onShow: '&'
  }
};

function Footer() {
  this.filters = [lotteryFilters.SHOW_ALL, lotteryFilters.SHOW_OPEN, lotteryFilters.SHOW_CLOSED];
  this.filterTitles = {};
  this.filterTitles[lotteryFilters.SHOW_ALL] = 'All';
  this.filterTitles[lotteryFilters.SHOW_OPEN] = 'Open';
  this.filterTitles[lotteryFilters.SHOW_CLOSED] = 'Closed';
}

Footer.prototype = {
  handleChange: function (filter) {
    this.onShow({filter: filter});
  }
};

