import { createBoard } from '@wixc3/react-board';
import App from '../../../app/layout/App';

export default createBoard({
    name: 'App',
    Board: () => <App />,
    isSnippet: true,
    environmentProps: {
canvasWidth: 511,
windowWidth: 1024
}
});