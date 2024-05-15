import { createTheme, CssBaseline, Container } from "@mui/material";
import { useState, useCallback, useEffect } from "react";
import { useLocation, Outlet } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { ThemeProvider } from "@mui/material/styles";
import HomePage from "../features/HomePage/HomePage";
import { useAppDispatch } from "../store/configureStore";
import LoadinComponent from "./LoadingComponent";
import ResponsiveAppBar from "./Navbar";
import { fetchCurrentUser } from "../features/account/accountSlice";

export default function App() {
  const theme = createTheme({
    palette: {
     mode:'dark'
    }
  })
  const locaiton = useLocation();
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  const initApp = useCallback(async () => {
    try {
      await dispatch(fetchCurrentUser());
    } catch (error: any) {
      console.log(error);
    }
  }, [dispatch])

  useEffect(() => {
    initApp().then(() => setLoading(false))
  }, [initApp])
  //<ToastContainer position="bottom-right" hideProgressBar theme="colored" />
  return (
    <ThemeProvider theme={theme}>
      
      <CssBaseline />
      <ResponsiveAppBar/>
      <CssBaseline />
      {loading ? <LoadinComponent message="Loading..." />
          : locaiton.pathname === '/' ? <HomePage />
          : <Container maxWidth={false} sx={{mx:0, my: 0, px: 0 , py: 0, pl: 0, pr: 0}}>
              <Outlet />
           </Container>
      }

    </ThemeProvider>
  );
}


