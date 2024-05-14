import { Box, Grid, Typography } from '@mui/material';
import Image from '../../../Images/GymPictureGood.jpg';  // Adjust the path if necessary
import { useState, useEffect, useCallback } from 'react';

export default function HomePage() {
  const [isSmallScreen, setIsSmallScreen] = useState(false);

  

  
  useEffect(() => {
    let width = window.innerWidth;

    console.log(width);

  },[window.innerWidth])

  useEffect(() => {
    const handleResize = () => {
      setIsSmallScreen(window.innerWidth <= 768); // Adjust the breakpoint as needed
    };


    // Call handleResize initially
    handleResize();

    window.addEventListener('resize', handleResize);

    // Cleanup the event listener on unmount
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  return (
    <div>
      <Grid
        container
        justifyContent="center"
        alignItems="center"
        sx={{
          height: '100vh', // Adjust for the height of the navbar
          width: '100vw',
          margin: 0,
          padding: 0,
          backgroundImage: `url(${Image})`,
          backgroundSize: 'cover',
          backgroundRepeat: 'no-repeat'
        }}
      >
        <Grid item xs={12} sx={{ height: '100%', position: 'relative' }}>
         <Box style={{color:'pink', opacity: '100%'}}>
         <Typography
            variant="h3"
            component="div"
            sx={{
              position: 'absolute',
              top: '40%',
              left: '35%',
              transform: 'translate(-50%, -50%)',
              fontSize: isSmallScreen ? '80px' : '160px', 
              color: 'transparent',
              backgroundImage: 'linear-gradient(#fff, #fff)',
              backgroundRepeat: 'no-repeat',
              WebkitBackgroundClip: 'text',
              backgroundPosition: '-800px 0',
              animation: 'backcolor 3s linear infinite alternate',
              '@keyframes backcolor': {
                '100%': {
                  backgroundPosition: '0 0',
                },
              },
            }}
          >
            Get Fit With Us
          </Typography>
         </Box>
        </Grid>
      </Grid>
    </div>
  );
}
