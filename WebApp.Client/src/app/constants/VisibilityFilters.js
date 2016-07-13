import lotteryFilters from './LotteryFilters.js';

function showAll() {
  return true;
}

function showClosed(draw) {
  return draw.draw_status === 'closed';
}

function showOpen(draw) {
  return draw.draw_status === 'open';
}

var filters = {};
filters[lotteryFilters.SHOW_ALL] = {filter: showAll, type: lotteryFilters.SHOW_ALL};
filters[lotteryFilters.SHOW_CLOSED] = {filter: showClosed, type: lotteryFilters.SHOW_CLOSED};
filters[lotteryFilters.SHOW_OPEN] = {filter: showOpen, type: lotteryFilters.SHOW_OPEN};

export default filters;
