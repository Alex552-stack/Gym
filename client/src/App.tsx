import { ThemeProvider, createTheme } from '@mui/material';
import './App.css'
import Navbar from './components/Navbar';
import SignInSide from './components/SignInSIde';


function App() {
  const theme = createTheme({
    palette:
    {
      mode: 'dark'
    }
  })
  return (
    <ThemeProvider theme = {theme}>
      <Navbar/>
      <SignInSide/>
      </ThemeProvider>
  )
}

export default App
