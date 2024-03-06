import { ThemeProvider, createTheme } from '@mui/material';
import './App.css'
import Navbar from './components/Navbar';


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
      </ThemeProvider>
  )
}

export default App
