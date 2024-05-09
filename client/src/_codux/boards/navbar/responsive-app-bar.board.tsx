import { createBoard } from '@wixc3/react-board';
import ResponsiveAppBar from '../../../app/layout/Navbar';

export default createBoard({
    name: 'ResponsiveAppBar',
    Board: () => <ResponsiveAppBar />,
    isSnippet: true,
    environmentProps: {
canvasWidth: 918,
canvasHeight: 671
}
});