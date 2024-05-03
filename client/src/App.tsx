import { ThemeProvider, createTheme } from '@mui/material';
import './App.css'
import Navbar from './components/Navbar';
import SignInSide from './components/SignInSIde';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import RegisterPage from './components/RegisterPage';


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
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<SignInSide />} />
          <Route path="/register" element={<RegisterPage/>} />
        </Routes>
      </BrowserRouter>
      </ThemeProvider>
  )
}

export default App
