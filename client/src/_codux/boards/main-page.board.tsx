import { createBoard } from '@wixc3/react-board';

export default createBoard({
    name: 'MainPage',
    Board: () => <div>
<img src="https://www.hussle.com/blog/wp-content/uploads/2020/12/Gym-structure-980x653.png" alt="" /></div>,
    isSnippet: true,
    environmentProps: {
canvasWidth: 1062.8650568181815,
canvasHeight: 643.2379261363635,
windowWidth: 1543
}
});