import lotteryFilters from '../constants/LotteryFilters';
import lotteries from '../lotteries/lotteries';

export default {
  templateUrl: 'src/app/containers/App.html',
  controller: App
};

function App() {
  this.draws = lotteries.initialDraws;
  this.filter = lotteryFilters.SHOW_ALL;
}
